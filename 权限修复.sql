-- =============================================
-- 博客权限修复SQL脚本
-- 日期: 2025-09-05
-- 说明: 为评论管理和博客配置添加缺失的权限验证
-- =============================================

-- 1. 添加评论编辑权限（如果不存在）
INSERT INTO `base_action` (`Id`, `CreateTime`, `CreatorId`, `Deleted`, `ParentId`, `Type`, `Name`, `Url`, `Value`, `NeedAction`, `Icon`, `Sort`) 
SELECT 'BLOG_COMMENT_EDIT', '2025-09-05 11:03:08', 'Admin', 0, 'BLOG_COMMENT_MENU', 2, '编辑评论', NULL, 'Blog_Comment.Edit', 1, NULL, 0
WHERE NOT EXISTS (SELECT 1 FROM `base_action` WHERE `Value` = 'Blog_Comment.Edit');

-- 2. 为超级管理员角色分配评论编辑权限（如果不存在）
INSERT INTO `base_roleaction` (`Id`, `CreateTime`, `CreatorId`, `Deleted`, `RoleId`, `ActionId`) 
SELECT 'BLOG_PERM_COMMENT_EDIT_FIX', '2025-09-05 17:00:00', 'Admin', 0, '1180486275199668224', 'BLOG_COMMENT_EDIT'
WHERE NOT EXISTS (
    SELECT 1 FROM `base_roleaction` 
    WHERE `RoleId` = '1180486275199668224' 
    AND `ActionId` = 'BLOG_COMMENT_EDIT'
);

-- 3. 验证权限是否添加成功
SELECT '权限添加完成，请检查以下结果:' as '执行状态';

-- 检查评论编辑权限是否存在
SELECT 
    CASE 
        WHEN EXISTS(SELECT 1 FROM `base_action` WHERE `Value` = 'Blog_Comment.Edit') 
        THEN '✓ 评论编辑权限已存在'
        ELSE '✗ 评论编辑权限添加失败'
    END as '权限检查';

-- 检查超级管理员是否有评论编辑权限
SELECT 
    CASE 
        WHEN EXISTS(
            SELECT 1 FROM `base_roleaction` ra
            JOIN `base_action` a ON ra.ActionId = a.Id
            WHERE ra.RoleId = '1180486275199668224' 
            AND a.Value = 'Blog_Comment.Edit'
        ) 
        THEN '✓ 超级管理员已有评论编辑权限'
        ELSE '✗ 超级管理员评论编辑权限分配失败'
    END as '角色权限检查';

-- 4. 显示当前超级管理员的所有博客相关权限（用于验证）
SELECT 
    a.Name as '权限名称',
    a.Value as '权限值',
    a.Type as '权限类型'
FROM `base_roleaction` ra
JOIN `base_action` a ON ra.ActionId = a.Id
WHERE ra.RoleId = '1180486275199668224' 
AND a.Value LIKE 'Blog_%'
ORDER BY a.ParentId, a.Sort;