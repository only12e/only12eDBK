-- 博客系统表结构一键修改脚本
-- 基于现有数据库表结构进行的必要调整
-- 执行前请先备份数据库！

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ========================================
-- 1. 修改技术栈表，添加content字段支持富文本
-- ========================================
ALTER TABLE `blog_technologies` 
ADD COLUMN `content` longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT '技术栈详细介绍，富文本，支持文字+图片' AFTER `description`;

-- 修改description字段注释，明确其用途
ALTER TABLE `blog_technologies` 
MODIFY COLUMN `description` text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT '技术简短描述，用于列表展示';

-- ========================================
-- 2. 修改评论表，添加对技术栈的支持
-- ========================================
ALTER TABLE `blog_comments` 
MODIFY COLUMN `target_type` enum('article','project','tool','technology') CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT '评论目标类型：文章/项目/工具/技术栈';

-- ========================================
-- 3. 修改点赞表，添加对技术栈的支持
-- ========================================
ALTER TABLE `blog_likes` 
MODIFY COLUMN `target_type` enum('article','project','tool','technology','comment') CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT '点赞目标类型：文章/项目/工具/技术栈/评论';

-- ========================================
-- 4. 修改浏览记录表，添加对技术栈的支持
-- ========================================
ALTER TABLE `blog_views` 
MODIFY COLUMN `target_type` enum('article','project','tool','technology') CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT '浏览目标类型：文章/项目/工具/技术栈';

-- ========================================
-- 5. 添加缺失的索引优化查询性能
-- ========================================
-- 为文章表添加性能索引
CREATE INDEX `idx_articles_status` ON `blog_articles`(`status`);
CREATE INDEX `idx_articles_featured` ON `blog_articles`(`is_featured`);

-- 为项目表添加性能索引
CREATE INDEX `idx_projects_featured` ON `blog_projects`(`is_featured`);

-- 为技术栈表添加性能索引
CREATE INDEX `idx_technologies_featured` ON `blog_technologies`(`is_featured`);

-- 为工具表添加性能索引
CREATE INDEX `idx_tools_recommended` ON `blog_tools`(`is_recommended`);

-- 为评论表添加性能索引
CREATE INDEX `idx_comments_target` ON `blog_comments`(`target_type`, `target_id`);

-- ========================================
-- 6. 确保外键约束正确（如果使用InnoDB引擎）
-- 注意：你当前使用的是MyISAM引擎，如需外键约束请改为InnoDB
-- ========================================

-- 可选：如果要启用外键约束，需要先改为InnoDB引擎
-- ALTER TABLE `blog_articles` ENGINE=InnoDB;
-- ALTER TABLE `blog_article_categories` ENGINE=InnoDB;
-- ALTER TABLE `blog_users` ENGINE=InnoDB;
-- ALTER TABLE `blog_technologies` ENGINE=InnoDB;
-- ALTER TABLE `blog_projects` ENGINE=InnoDB;
-- ALTER TABLE `blog_project_technologies` ENGINE=InnoDB;
-- ALTER TABLE `blog_tools` ENGINE=InnoDB;
-- ALTER TABLE `blog_comments` ENGINE=InnoDB;
-- ALTER TABLE `blog_likes` ENGINE=InnoDB;
-- ALTER TABLE `blog_views` ENGINE=InnoDB;

-- 然后添加外键约束
-- ALTER TABLE `blog_articles` ADD CONSTRAINT `fk_articles_category` FOREIGN KEY (`category_id`) REFERENCES `blog_article_categories`(`id`);
-- ALTER TABLE `blog_articles` ADD CONSTRAINT `fk_articles_author` FOREIGN KEY (`author_id`) REFERENCES `blog_users`(`id`);
-- ALTER TABLE `blog_project_technologies` ADD CONSTRAINT `fk_project_tech_project` FOREIGN KEY (`project_id`) REFERENCES `blog_projects`(`id`) ON DELETE CASCADE;
-- ALTER TABLE `blog_project_technologies` ADD CONSTRAINT `fk_project_tech_technology` FOREIGN KEY (`technology_id`) REFERENCES `blog_technologies`(`id`);
-- ALTER TABLE `blog_comments` ADD CONSTRAINT `fk_comments_user` FOREIGN KEY (`user_id`) REFERENCES `blog_users`(`id`);
-- ALTER TABLE `blog_comments` ADD CONSTRAINT `fk_comments_parent` FOREIGN KEY (`parent_id`) REFERENCES `blog_comments`(`id`) ON DELETE SET NULL;
-- ALTER TABLE `blog_likes` ADD CONSTRAINT `fk_likes_user` FOREIGN KEY (`user_id`) REFERENCES `blog_users`(`id`);

-- ========================================
-- 7. 验证修改结果
-- ========================================
-- 检查技术栈表结构
-- DESCRIBE `blog_technologies`;

-- 检查评论表的target_type字段
-- SHOW COLUMNS FROM `blog_comments` LIKE 'target_type';

-- 检查点赞表的target_type字段
-- SHOW COLUMNS FROM `blog_likes` LIKE 'target_type';

-- 检查浏览表的target_type字段
-- SHOW COLUMNS FROM `blog_views` LIKE 'target_type';

-- 检查所有索引是否创建成功
-- SHOW INDEX FROM `blog_articles`;
-- SHOW INDEX FROM `blog_projects`;
-- SHOW INDEX FROM `blog_technologies`;
-- SHOW INDEX FROM `blog_tools`;
-- SHOW INDEX FROM `blog_comments`;

SET FOREIGN_KEY_CHECKS = 1;

-- ========================================
-- 修改完成提示
-- ========================================
/*
修改完成！主要变更内容：

1. ✅ 技术栈表增加了content字段，现在支持富文本详细介绍
2. ✅ 评论系统扩展支持技术栈评论
3. ✅ 点赞系统扩展支持技术栈和评论点赞
4. ✅ 浏览统计扩展支持技术栈浏览记录
5. ✅ 添加了性能优化索引
6. ✅ 现在所有四个模块（文章、项目、工具、技术栈）都完全支持富文本内容

现在你的数据库结构完全符合博客系统的设计要求！

如需启用外键约束增强数据完整性，请取消注释第6部分的SQL语句并执行。
*/