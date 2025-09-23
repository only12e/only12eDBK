-- 数据库ID类型统一迁移脚本
-- 解决管理系统和博客系统数据不同步问题

-- 注意：请根据您的实际数据库名称修改下面的USE语句
-- USE `colder.admin.antdvue`;  -- 如果数据库名称包含点号，需要用反引号包围

-- 备份当前数据（可选）
-- CREATE TABLE blog_tools_backup AS SELECT * FROM blog_tools;
-- CREATE TABLE blog_technologies_backup AS SELECT * FROM blog_technologies;

-- 1. 修改 blog_tools 表ID类型为 bigint(20)
ALTER TABLE `blog_tools` MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '工具ID，主键';

-- 2. 修改 blog_technologies 表ID类型为 bigint(20)
ALTER TABLE `blog_technologies` MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '技术栈ID，主键';

-- 3. 确保所有博客相关表的ID类型一致
-- 检查并确认以下表的ID类型都是 bigint(20):
-- blog_articles (已经是 bigint(20))
-- blog_projects (已经是 bigint(20))
-- blog_comments (已经是 bigint(20))
-- blog_likes (已经是 bigint(20))

-- 4. 更新 blog_comments 表的索引（如果需要）
-- 由于 target_id 现在统一为 bigint(20)，确保索引正确
-- 安全删除现有索引（兼容旧版MySQL）
SET @sql = (SELECT IF(
    (SELECT COUNT(*) FROM INFORMATION_SCHEMA.STATISTICS
     WHERE table_schema = DATABASE()
     AND table_name = 'blog_comments'
     AND index_name = 'idx_comments_target') > 0,
    'DROP INDEX `idx_comments_target` ON `blog_comments`',
    'SELECT "索引 idx_comments_target 不存在"'
));
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

CREATE INDEX `idx_comments_target` ON `blog_comments`(`target_type`, `target_id`);

-- 5. 更新 blog_likes 表的索引（如果需要）
-- 安全删除现有索引（兼容旧版MySQL）
SET @sql = (SELECT IF(
    (SELECT COUNT(*) FROM INFORMATION_SCHEMA.STATISTICS
     WHERE table_schema = DATABASE()
     AND table_name = 'blog_likes'
     AND index_name = 'idx_target_likes') > 0,
    'DROP INDEX `idx_target_likes` ON `blog_likes`',
    'SELECT "索引 idx_target_likes 不存在"'
));
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

CREATE INDEX `idx_target_likes` ON `blog_likes`(`target_type`, `target_id`);

-- 验证修改结果
SELECT
    TABLE_NAME,
    COLUMN_NAME,
    DATA_TYPE,
    COLUMN_TYPE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = DATABASE()  -- 使用当前数据库
  AND TABLE_NAME IN ('blog_articles', 'blog_projects', 'blog_tools', 'blog_technologies', 'blog_comments', 'blog_likes')
  AND COLUMN_NAME = 'id'
ORDER BY TABLE_NAME;

-- 预期结果：所有表的ID字段都应该是 bigint(20)