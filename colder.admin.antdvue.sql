/*
 Navicat Premium Dump SQL

 Source Server         : work
 Source Server Type    : MySQL
 Source Server Version : 80012 (8.0.12)
 Source Host           : localhost:3306
 Source Schema         : colder.admin.antdvue

 Target Server Type    : MySQL
 Target Server Version : 80012 (8.0.12)
 File Encoding         : 65001

 Date: 22/09/2025 11:01:26
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for base_action
-- ----------------------------
DROP TABLE IF EXISTS `base_action`;
CREATE TABLE `base_action`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '主键',
  `CreateTime` datetime NOT NULL COMMENT '创建时间',
  `CreatorId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人Id',
  `Deleted` tinyint(4) NOT NULL DEFAULT 0 COMMENT '否已删除',
  `ParentId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '父级Id',
  `Type` int(11) NOT NULL COMMENT '类型,菜单=0,页面=1,权限=2',
  `Name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '权限名/菜单名',
  `Url` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '菜单地址',
  `Value` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '权限值',
  `NeedAction` tinyint(4) NOT NULL DEFAULT 0 COMMENT '是否需要权限(仅页面有效)',
  `Icon` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图标',
  `Sort` int(11) NOT NULL DEFAULT 0 COMMENT '排序',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '系统权限表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of base_action
-- ----------------------------
INSERT INTO `base_action` VALUES ('1178957405992521728', '2019-10-01 16:58:44', NULL, 0, NULL, 0, '系统管理', '', NULL, 1, 'setting', 1);
INSERT INTO `base_action` VALUES ('1178957553778823168', '2019-10-01 16:59:19', NULL, 0, '1178957405992521728', 1, '权限管理', '/Base_Manage/Base_Action/List', NULL, 1, NULL, 20);
INSERT INTO `base_action` VALUES ('1179018395304071168', '2019-10-01 21:01:05', NULL, 0, '1178957405992521728', 1, '密钥管理', '/Base_Manage/Base_AppSecret/List', NULL, 1, NULL, 15);
INSERT INTO `base_action` VALUES ('1182652266117599232', '2019-10-11 21:40:47', NULL, 0, '1178957405992521728', 1, '用户管理', '/Base_Manage/Base_User/List', NULL, 1, NULL, 0);
INSERT INTO `base_action` VALUES ('1182652367447789568', '2019-10-11 21:41:11', NULL, 0, '1178957405992521728', 1, '角色管理', '/Base_Manage/Base_Role/List', NULL, 1, NULL, 5);
INSERT INTO `base_action` VALUES ('1182652433302556672', '2019-10-11 21:41:27', NULL, 0, '1178957405992521728', 1, '部门管理', '/Base_Manage/Base_Department/List', NULL, 1, NULL, 10);
INSERT INTO `base_action` VALUES ('1188801057778569216', '2019-10-28 20:53:53', NULL, 0, '1182652367447789568', 2, '增', NULL, 'Base_Role.Add', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('1188801057778569217', '2019-10-28 20:53:53', NULL, 0, '1182652367447789568', 2, '改', NULL, 'Base_Role.Edit', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('1188801057778569218', '2019-10-28 20:53:53', NULL, 0, '1182652367447789568', 2, '删', NULL, 'Base_Role.Delete', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('1188801109783744512', '2019-10-28 20:54:06', NULL, 0, '1182652433302556672', 2, '增', NULL, 'Base_Department.Add', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('1188801109783744513', '2019-10-28 20:54:06', NULL, 0, '1182652433302556672', 2, '改', NULL, 'Base_Department.Edit', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('1188801109783744514', '2019-10-28 20:54:06', NULL, 0, '1182652433302556672', 2, '删', NULL, 'Base_Department.Delete', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('1188801273885888512', '2019-10-28 20:54:45', NULL, 0, '1179018395304071168', 2, '增', NULL, 'Base_AppSecret.Add', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('1188801273885888513', '2019-10-28 20:54:45', NULL, 0, '1179018395304071168', 2, '改', NULL, 'Base_AppSecret.Edit', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('1188801273885888514', '2019-10-28 20:54:45', NULL, 0, '1179018395304071168', 2, '删', NULL, 'Base_AppSecret.Delete', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('1188801341661646848', '2019-10-28 20:55:01', NULL, 0, '1178957553778823168', 2, '增', NULL, 'Base_Action.Add', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('1188801341661646849', '2019-10-28 20:55:01', NULL, 0, '1178957553778823168', 2, '改', NULL, 'Base_Action.Edit', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('1188801341661646850', '2019-10-28 20:55:01', NULL, 0, '1178957553778823168', 2, '删', NULL, 'Base_Action.Delete', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('1193158266167758848', '2019-11-09 21:27:53', 'Admin', 0, NULL, 0, '首页', NULL, NULL, 1, 'home', 0);
INSERT INTO `base_action` VALUES ('1251792477787000832', '2020-04-19 16:39:19', NULL, 0, '1182652266117599232', 2, '增', NULL, 'Base_User.Add', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('1251792477787000833', '2020-04-19 16:39:19', NULL, 0, '1182652266117599232', 2, '改', NULL, 'Base_User.Edit', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('1251792477787000834', '2020-04-19 16:39:19', NULL, 0, '1182652266117599232', 2, '删', NULL, 'Base_User.Delete', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('1251792721769664512', '2020-04-19 16:40:17', 'Admin', 0, '1178957405992521728', 1, '操作日志', '/Base_Manage/Base_UserLog/List', NULL, 0, NULL, 23);
INSERT INTO `base_action` VALUES ('1964931379573559296', '2025-09-08 13:58:42', NULL, 0, 'BLOG_COMMENT_MENU', 2, '编辑评论', NULL, 'Blog_Comment.Edit', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('1964931379573559297', '2025-09-08 13:58:42', NULL, 0, 'BLOG_COMMENT_MENU', 2, '评论审核', NULL, 'Blog_Comment.Approve', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('1964931379573559298', '2025-09-08 13:58:42', NULL, 0, 'BLOG_COMMENT_MENU', 2, '删除评论', NULL, 'Blog_Comment.Delete', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('1964931379573559299', '2025-09-08 13:58:42', NULL, 0, 'BLOG_COMMENT_MENU', 2, '新建评论', NULL, 'Blog_Comment.Add', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('BLOG_ARTICLE_ADD', '2025-09-05 11:03:08', 'Admin', 0, 'BLOG_ARTICLE_MENU', 2, '新增文章', NULL, 'Blog_Article.Add', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('BLOG_ARTICLE_DELETE', '2025-09-05 11:03:08', 'Admin', 0, 'BLOG_ARTICLE_MENU', 2, '删除文章', NULL, 'Blog_Article.Delete', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('BLOG_ARTICLE_EDIT', '2025-09-05 11:03:08', 'Admin', 0, 'BLOG_ARTICLE_MENU', 2, '编辑文章', NULL, 'Blog_Article.Edit', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('BLOG_ARTICLE_MENU', '2025-09-05 11:03:08', 'Admin', 0, 'BLOG_MAIN_MENU', 1, '文章管理', '/Blog_Manage/Blog_Article/List', NULL, 1, NULL, 10);
INSERT INTO `base_action` VALUES ('BLOG_ARTICLE_PUBLISH', '2025-09-05 11:03:08', 'Admin', 0, 'BLOG_ARTICLE_MENU', 2, '发布文章', NULL, 'Blog_Article.Publish', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('BLOG_COMMENT_MENU', '2025-09-05 11:03:08', 'Admin', 0, 'BLOG_MAIN_MENU', 1, '评论管理', '/Blog_Manage/Blog_Comment/List', NULL, 1, NULL, 50);
INSERT INTO `base_action` VALUES ('BLOG_MAIN_MENU', '2025-09-05 11:03:08', 'Admin', 0, NULL, 0, '博客管理', '', NULL, 1, 'book', 100);
INSERT INTO `base_action` VALUES ('BLOG_PROJECT_ADD', '2025-09-05 11:03:08', 'Admin', 0, 'BLOG_PROJECT_MENU', 2, '新增项目', NULL, 'Blog_Project.Add', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('BLOG_PROJECT_DELETE', '2025-09-05 11:03:08', 'Admin', 0, 'BLOG_PROJECT_MENU', 2, '删除项目', NULL, 'Blog_Project.Delete', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('BLOG_PROJECT_EDIT', '2025-09-05 11:03:08', 'Admin', 0, 'BLOG_PROJECT_MENU', 2, '编辑项目', NULL, 'Blog_Project.Edit', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('BLOG_PROJECT_MENU', '2025-09-05 11:03:08', 'Admin', 0, 'BLOG_MAIN_MENU', 1, '项目管理', '/Blog_Manage/Blog_Project/List', NULL, 1, NULL, 20);
INSERT INTO `base_action` VALUES ('BLOG_STATS_MENU', '2025-09-05 11:03:08', 'Admin', 0, '1193158266167758848', 1, '数据统计', '/Blog_Manage/Blog_Statistics/Dashboard', NULL, 1, NULL, 90);
INSERT INTO `base_action` VALUES ('BLOG_TECH_ADD', '2025-09-05 11:03:08', 'Admin', 0, 'BLOG_TECH_MENU', 2, '新增技术栈', NULL, 'Blog_Technology.Add', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('BLOG_TECH_DELETE', '2025-09-05 11:03:08', 'Admin', 0, 'BLOG_TECH_MENU', 2, '删除技术栈', NULL, 'Blog_Technology.Delete', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('BLOG_TECH_EDIT', '2025-09-05 11:03:08', 'Admin', 0, 'BLOG_TECH_MENU', 2, '编辑技术栈', NULL, 'Blog_Technology.Edit', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('BLOG_TECH_MENU', '2025-09-05 11:03:08', 'Admin', 0, 'BLOG_MAIN_MENU', 1, '技术栈管理', '/Blog_Manage/Blog_Technology/List', NULL, 1, NULL, 40);
INSERT INTO `base_action` VALUES ('BLOG_TOOL_ADD', '2025-09-05 11:03:08', 'Admin', 0, 'BLOG_TOOL_MENU', 2, '新增工具', NULL, 'Blog_Tool.Add', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('BLOG_TOOL_DELETE', '2025-09-05 11:03:08', 'Admin', 0, 'BLOG_TOOL_MENU', 2, '删除工具', NULL, 'Blog_Tool.Delete', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('BLOG_TOOL_EDIT', '2025-09-05 11:03:08', 'Admin', 0, 'BLOG_TOOL_MENU', 2, '编辑工具', NULL, 'Blog_Tool.Edit', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('BLOG_TOOL_MENU', '2025-09-05 11:03:08', 'Admin', 0, 'BLOG_MAIN_MENU', 1, '工具管理', '/Blog_Manage/Blog_Tool/List', NULL, 1, NULL, 30);

-- ----------------------------
-- Table structure for base_appsecret
-- ----------------------------
DROP TABLE IF EXISTS `base_appsecret`;
CREATE TABLE `base_appsecret`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '自然主键',
  `CreateTime` datetime NOT NULL COMMENT '创建时间',
  `CreatorId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人Id',
  `Deleted` tinyint(4) NOT NULL DEFAULT 0 COMMENT '否已删除',
  `AppId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '应用Id',
  `AppSecret` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '应用密钥',
  `AppName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '应用名',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '应用密钥表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of base_appsecret
-- ----------------------------
INSERT INTO `base_appsecret` VALUES ('1172497995938271232', '2019-09-13 21:11:20', 'Admin', 0, 'PcAdmin', 'wtMaiTRPTT3hrf5e', '后台AppId');
INSERT INTO `base_appsecret` VALUES ('1173937877642383360', '2019-09-17 20:32:55', 'Admin', 0, 'AppAdmin', 'IVh9LLSVFcoQPQ5K', 'APP密钥');

-- ----------------------------
-- Table structure for base_buildtest
-- ----------------------------
DROP TABLE IF EXISTS `base_buildtest`;
CREATE TABLE `base_buildtest`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '自然主键',
  `CreateTime` datetime NOT NULL COMMENT '创建时间',
  `CreatorId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人Id',
  `Deleted` tinyint(4) NOT NULL DEFAULT 0 COMMENT '否已删除',
  `Column1` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '列1',
  `Column2` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '列2',
  `Column3` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '列3',
  `Column4` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '列4',
  `Column5` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '列5',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '生成测试表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of base_buildtest
-- ----------------------------

-- ----------------------------
-- Table structure for base_dblink
-- ----------------------------
DROP TABLE IF EXISTS `base_dblink`;
CREATE TABLE `base_dblink`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '自然主键',
  `CreateTime` datetime NOT NULL COMMENT '创建时间',
  `CreatorId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人Id',
  `Deleted` tinyint(4) NOT NULL DEFAULT 0 COMMENT '否已删除',
  `LinkName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '连接名',
  `ConnectionStr` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '连接字符串',
  `DbType` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '数据库类型',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '数据库连接表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of base_dblink
-- ----------------------------
INSERT INTO `base_dblink` VALUES ('1962823952422146048', '2025-09-02 18:24:32', 'Admin', 0, 'learn', 'Data Source=localhost;Database=colder.admin.antdvue;User ID=root;Password=123456;pooling=true;port=3306;sslmode=none;CharSet=utf8;', 'MySql');

-- ----------------------------
-- Table structure for base_department
-- ----------------------------
DROP TABLE IF EXISTS `base_department`;
CREATE TABLE `base_department`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '主键',
  `CreateTime` datetime NOT NULL COMMENT '创建时间',
  `CreatorId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人Id',
  `Deleted` tinyint(4) NOT NULL DEFAULT 0 COMMENT '否已删除',
  `Name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '部门名',
  `ParentId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '上级部门Id',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '部门表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of base_department
-- ----------------------------

-- ----------------------------
-- Table structure for base_role
-- ----------------------------
DROP TABLE IF EXISTS `base_role`;
CREATE TABLE `base_role`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '主键',
  `CreateTime` datetime NOT NULL COMMENT '创建时间',
  `CreatorId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人Id',
  `Deleted` tinyint(4) NOT NULL DEFAULT 0 COMMENT '否已删除',
  `RoleName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '角色名',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '系统角色表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of base_role
-- ----------------------------
INSERT INTO `base_role` VALUES ('1180486275199668224', '2019-10-05 22:13:55', NULL, 0, '超级管理员');
INSERT INTO `base_role` VALUES ('1180819481383931904', '2019-10-06 20:17:57', NULL, 0, '用户');

-- ----------------------------
-- Table structure for base_roleaction
-- ----------------------------
DROP TABLE IF EXISTS `base_roleaction`;
CREATE TABLE `base_roleaction`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '主键',
  `CreateTime` datetime NOT NULL COMMENT '创建时间',
  `CreatorId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人Id',
  `Deleted` tinyint(4) NOT NULL DEFAULT 0 COMMENT '否已删除',
  `RoleId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户Id',
  `ActionId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '权限Id',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '角色权限表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of base_roleaction
-- ----------------------------
INSERT INTO `base_roleaction` VALUES ('1964958559787552768', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', '1188044797802188800');
INSERT INTO `base_roleaction` VALUES ('1964958559787552769', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', '1188044797802188801');
INSERT INTO `base_roleaction` VALUES ('1964958559787552770', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', '1188800845714558976');
INSERT INTO `base_roleaction` VALUES ('1964958559787552771', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', '1188800845714558977');
INSERT INTO `base_roleaction` VALUES ('1964958559787552772', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', '1188800845714558978');
INSERT INTO `base_roleaction` VALUES ('1964958559787552773', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', '1193158266167758848');
INSERT INTO `base_roleaction` VALUES ('1964958559787552774', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_STATS_MENU');
INSERT INTO `base_roleaction` VALUES ('1964958559787552775', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_MAIN_MENU');
INSERT INTO `base_roleaction` VALUES ('1964958559787552776', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_ARTICLE_MENU');
INSERT INTO `base_roleaction` VALUES ('1964958559787552777', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_PROJECT_MENU');
INSERT INTO `base_roleaction` VALUES ('1964958559787552778', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_TOOL_MENU');
INSERT INTO `base_roleaction` VALUES ('1964958559787552779', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_TECH_MENU');
INSERT INTO `base_roleaction` VALUES ('1964958559787552780', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_COMMENT_MENU');
INSERT INTO `base_roleaction` VALUES ('1964958559787552781', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_USER_MENU');
INSERT INTO `base_roleaction` VALUES ('1964958559787552782', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_CONFIG_MENU');
INSERT INTO `base_roleaction` VALUES ('1964958559787552783', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_LINK_MENU');
INSERT INTO `base_roleaction` VALUES ('1964958559787552784', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_ARTICLE_ADD');
INSERT INTO `base_roleaction` VALUES ('1964958559787552785', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_ARTICLE_EDIT');
INSERT INTO `base_roleaction` VALUES ('1964958559787552786', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_ARTICLE_DELETE');
INSERT INTO `base_roleaction` VALUES ('1964958559787552787', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_ARTICLE_PUBLISH');
INSERT INTO `base_roleaction` VALUES ('1964958559787552788', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_PROJECT_ADD');
INSERT INTO `base_roleaction` VALUES ('1964958559787552789', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_PROJECT_EDIT');
INSERT INTO `base_roleaction` VALUES ('1964958559787552790', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_PROJECT_DELETE');
INSERT INTO `base_roleaction` VALUES ('1964958559787552791', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_TOOL_ADD');
INSERT INTO `base_roleaction` VALUES ('1964958559787552792', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_TOOL_EDIT');
INSERT INTO `base_roleaction` VALUES ('1964958559787552793', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_TOOL_DELETE');
INSERT INTO `base_roleaction` VALUES ('1964958559787552794', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_TECH_ADD');
INSERT INTO `base_roleaction` VALUES ('1964958559787552795', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_TECH_EDIT');
INSERT INTO `base_roleaction` VALUES ('1964958559787552796', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_TECH_DELETE');
INSERT INTO `base_roleaction` VALUES ('1964958559787552797', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', '1964931379573559296');
INSERT INTO `base_roleaction` VALUES ('1964958559787552798', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', '1964931379573559297');
INSERT INTO `base_roleaction` VALUES ('1964958559787552799', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', '1964931379573559298');
INSERT INTO `base_roleaction` VALUES ('1964958559787552800', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', '1964931379573559299');
INSERT INTO `base_roleaction` VALUES ('1964958559787552801', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_USER_ADD');
INSERT INTO `base_roleaction` VALUES ('1964958559787552802', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_USER_EDIT');
INSERT INTO `base_roleaction` VALUES ('1964958559787552803', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_USER_DELETE');
INSERT INTO `base_roleaction` VALUES ('1964958559787552804', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_USER_STATUS');
INSERT INTO `base_roleaction` VALUES ('1964958559787552805', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', '1964930733755600896');
INSERT INTO `base_roleaction` VALUES ('1964958559787552806', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', '1964930733755600897');
INSERT INTO `base_roleaction` VALUES ('1964958559787552807', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', '1964930733755600898');
INSERT INTO `base_roleaction` VALUES ('1964958559787552808', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', '1964930733755600899');
INSERT INTO `base_roleaction` VALUES ('1964958559787552809', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', '1964930733755600900');
INSERT INTO `base_roleaction` VALUES ('1964958559787552810', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_LINK_ADD');
INSERT INTO `base_roleaction` VALUES ('1964958559787552811', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_LINK_EDIT');
INSERT INTO `base_roleaction` VALUES ('1964958559787552812', '2025-09-08 15:46:42', NULL, 0, '1180819481383931904', 'BLOG_LINK_DELETE');
INSERT INTO `base_roleaction` VALUES ('1968937075638013952', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1178957405992521728');
INSERT INTO `base_roleaction` VALUES ('1968937075638013953', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1178957553778823168');
INSERT INTO `base_roleaction` VALUES ('1968937075638013954', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1179018395304071168');
INSERT INTO `base_roleaction` VALUES ('1968937075638013955', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1182652367447789568');
INSERT INTO `base_roleaction` VALUES ('1968937075638013956', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1182652433302556672');
INSERT INTO `base_roleaction` VALUES ('1968937075638013957', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1188801057778569216');
INSERT INTO `base_roleaction` VALUES ('1968937075638013958', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1188801057778569217');
INSERT INTO `base_roleaction` VALUES ('1968937075638013959', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1188801057778569218');
INSERT INTO `base_roleaction` VALUES ('1968937075638013960', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1188801109783744512');
INSERT INTO `base_roleaction` VALUES ('1968937075638013961', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1188801109783744513');
INSERT INTO `base_roleaction` VALUES ('1968937075638013962', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1188801109783744514');
INSERT INTO `base_roleaction` VALUES ('1968937075638013963', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1188801273885888512');
INSERT INTO `base_roleaction` VALUES ('1968937075638013964', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1188801273885888513');
INSERT INTO `base_roleaction` VALUES ('1968937075638013965', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1188801273885888514');
INSERT INTO `base_roleaction` VALUES ('1968937075638013966', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1188801341661646848');
INSERT INTO `base_roleaction` VALUES ('1968937075638013967', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1188801341661646849');
INSERT INTO `base_roleaction` VALUES ('1968937075638013968', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1188801341661646850');
INSERT INTO `base_roleaction` VALUES ('1968937075638013969', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1193158266167758848');
INSERT INTO `base_roleaction` VALUES ('1968937075638013970', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1251792721769664512');
INSERT INTO `base_roleaction` VALUES ('1968937075638013971', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1964931379573559296');
INSERT INTO `base_roleaction` VALUES ('1968937075638013972', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1964931379573559297');
INSERT INTO `base_roleaction` VALUES ('1968937075638013973', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1964931379573559298');
INSERT INTO `base_roleaction` VALUES ('1968937075638013974', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1964931379573559299');
INSERT INTO `base_roleaction` VALUES ('1968937075638013975', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1965239825674014720');
INSERT INTO `base_roleaction` VALUES ('1968937075638013976', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1965239825674014721');
INSERT INTO `base_roleaction` VALUES ('1968937075638013977', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1965239825674014722');
INSERT INTO `base_roleaction` VALUES ('1968937075638013978', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1965239825674014723');
INSERT INTO `base_roleaction` VALUES ('1968937075638013979', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', '1965239825674014724');
INSERT INTO `base_roleaction` VALUES ('1968937075638013980', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_ARTICLE_ADD');
INSERT INTO `base_roleaction` VALUES ('1968937075638013981', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_ARTICLE_DELETE');
INSERT INTO `base_roleaction` VALUES ('1968937075638013982', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_ARTICLE_EDIT');
INSERT INTO `base_roleaction` VALUES ('1968937075638013983', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_ARTICLE_MENU');
INSERT INTO `base_roleaction` VALUES ('1968937075638013984', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_ARTICLE_PUBLISH');
INSERT INTO `base_roleaction` VALUES ('1968937075638013985', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_COMMENT_MENU');
INSERT INTO `base_roleaction` VALUES ('1968937075638013986', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_CONFIG_MENU');
INSERT INTO `base_roleaction` VALUES ('1968937075638013987', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_LINK_ADD');
INSERT INTO `base_roleaction` VALUES ('1968937075638013988', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_LINK_DELETE');
INSERT INTO `base_roleaction` VALUES ('1968937075638013989', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_LINK_EDIT');
INSERT INTO `base_roleaction` VALUES ('1968937075638013990', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_LINK_MENU');
INSERT INTO `base_roleaction` VALUES ('1968937075638013991', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_MAIN_MENU');
INSERT INTO `base_roleaction` VALUES ('1968937075638013992', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_PROJECT_ADD');
INSERT INTO `base_roleaction` VALUES ('1968937075638013993', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_PROJECT_DELETE');
INSERT INTO `base_roleaction` VALUES ('1968937075638013994', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_PROJECT_EDIT');
INSERT INTO `base_roleaction` VALUES ('1968937075638013995', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_PROJECT_MENU');
INSERT INTO `base_roleaction` VALUES ('1968937075638013996', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_STATS_MENU');
INSERT INTO `base_roleaction` VALUES ('1968937075638013997', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_TECH_ADD');
INSERT INTO `base_roleaction` VALUES ('1968937075638013998', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_TECH_DELETE');
INSERT INTO `base_roleaction` VALUES ('1968937075638013999', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_TECH_EDIT');
INSERT INTO `base_roleaction` VALUES ('1968937075638014000', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_TECH_MENU');
INSERT INTO `base_roleaction` VALUES ('1968937075638014001', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_TOOL_ADD');
INSERT INTO `base_roleaction` VALUES ('1968937075638014002', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_TOOL_DELETE');
INSERT INTO `base_roleaction` VALUES ('1968937075638014003', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_TOOL_EDIT');
INSERT INTO `base_roleaction` VALUES ('1968937075638014004', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_TOOL_MENU');
INSERT INTO `base_roleaction` VALUES ('1968937075638014005', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_USER_ADD');
INSERT INTO `base_roleaction` VALUES ('1968937075638014006', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_USER_DELETE');
INSERT INTO `base_roleaction` VALUES ('1968937075638014007', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_USER_EDIT');
INSERT INTO `base_roleaction` VALUES ('1968937075638014008', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_USER_MENU');
INSERT INTO `base_roleaction` VALUES ('1968937075638014009', '2025-09-19 15:15:55', NULL, 0, '1180486275199668224', 'BLOG_USER_STATUS');

-- ----------------------------
-- Table structure for base_user
-- ----------------------------
DROP TABLE IF EXISTS `base_user`;
CREATE TABLE `base_user`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '主键',
  `CreateTime` datetime NOT NULL COMMENT '创建时间',
  `CreatorId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人Id',
  `Deleted` tinyint(4) NOT NULL DEFAULT 0 COMMENT '否已删除',
  `UserName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户名',
  `Password` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '密码',
  `RealName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '姓名',
  `Sex` int(11) NOT NULL DEFAULT 0 COMMENT '性别(1为男，0为女)',
  `Birthday` date NULL DEFAULT NULL COMMENT '出生日期',
  `DepartmentId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '所属部门Id',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '系统用户表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of base_user
-- ----------------------------
INSERT INTO `base_user` VALUES ('1181928860648738816', '2019-10-09 21:46:14', NULL, 0, 'x', '670b14728ad9902aecba32e22fa4f6bd', '花', 0, NULL, NULL);
INSERT INTO `base_user` VALUES ('1968968067677425664', '2025-09-19 17:19:04', 'Admin', 0, 'root', '670b14728ad9902aecba32e22fa4f6bd', 'r', 1, NULL, NULL);
INSERT INTO `base_user` VALUES ('Admin', '2019-09-13 21:10:03', 'Admin', 0, 'Admin', 'e10adc3949ba59abbe56e057f20f883e', '超级管理员', 1, '2019-09-13', NULL);

-- ----------------------------
-- Table structure for base_userlog
-- ----------------------------
DROP TABLE IF EXISTS `base_userlog`;
CREATE TABLE `base_userlog`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '自然主键',
  `CreateTime` datetime NOT NULL COMMENT '创建时间',
  `CreatorId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人Id',
  `CreatorRealName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人姓名',
  `LogType` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '日志类型',
  `LogContent` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '日志内容',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '系统日志表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of base_userlog
-- ----------------------------
INSERT INTO `base_userlog` VALUES ('1963830827057942528', '2025-09-05 13:05:30', 'Admin', '超级管理员', '博客管理', '添加文章:发顺丰');
INSERT INTO `base_userlog` VALUES ('1963830940237041664', '2025-09-05 13:05:57', 'Admin', '超级管理员', '博客管理', '修改文章:发顺丰肺癌');
INSERT INTO `base_userlog` VALUES ('1963832404497928192', '2025-09-05 13:11:46', 'Admin', '超级管理员', '博客管理', '添加文章:法法师');
INSERT INTO `base_userlog` VALUES ('1963833103898120192', '2025-09-05 13:14:33', 'Admin', '超级管理员', '博客管理', '添加文章:分数分身乏术');
INSERT INTO `base_userlog` VALUES ('1963833133526683648', '2025-09-05 13:14:40', 'Admin', '超级管理员', '博客管理', '修改文章:分数分身乏术');
INSERT INTO `base_userlog` VALUES ('1963833544484589568', '2025-09-05 13:16:18', 'Admin', '超级管理员', '博客管理', '修改文章:MySQL性能优化实战指南');
INSERT INTO `base_userlog` VALUES ('1963835620954476544', '2025-09-05 13:24:33', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1963835734880161792', '2025-09-05 13:25:00', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1963835774143041536', '2025-09-05 13:25:10', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1963835871916462080', '2025-09-05 13:25:33', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1963836047825571840', '2025-09-05 13:26:15', 'Admin', '超级管理员', '博客管理', '添加文章:份数');
INSERT INTO `base_userlog` VALUES ('1963836135390056448', '2025-09-05 13:26:36', 'Admin', '超级管理员', '博客管理', '添加文章:非法方式');
INSERT INTO `base_userlog` VALUES ('1963836164766961664', '2025-09-05 13:26:43', 'Admin', '超级管理员', '博客管理', '添加文章:发');
INSERT INTO `base_userlog` VALUES ('1963836189836316672', '2025-09-05 13:26:49', 'Admin', '超级管理员', '博客管理', '添加文章:发色');
INSERT INTO `base_userlog` VALUES ('1963836212502335488', '2025-09-05 13:26:54', 'Admin', '超级管理员', '博客管理', '添加文章:飞阿斯');
INSERT INTO `base_userlog` VALUES ('1963836247617048576', '2025-09-05 13:27:02', 'Admin', '超级管理员', '博客管理', '添加文章:发色肺癌');
INSERT INTO `base_userlog` VALUES ('1963836311475326976', '2025-09-05 13:27:18', 'Admin', '超级管理员', '博客管理', '添加文章:额发生飞阿斯');
INSERT INTO `base_userlog` VALUES ('1963836348485865472', '2025-09-05 13:27:26', 'Admin', '超级管理员', '博客管理', '添加文章:分啊色法');
INSERT INTO `base_userlog` VALUES ('1963836391628476416', '2025-09-05 13:27:37', 'Admin', '超级管理员', '博客管理', '添加文章:发色丰富');
INSERT INTO `base_userlog` VALUES ('1963836451451834368', '2025-09-05 13:27:51', 'Admin', '超级管理员', '博客管理', '修改文章:份数');
INSERT INTO `base_userlog` VALUES ('1963838285474172928', '2025-09-05 13:35:08', 'Admin', '超级管理员', '博客管理', '修改文章:Redis缓存策略与最佳实践');
INSERT INTO `base_userlog` VALUES ('1963838727268601856', '2025-09-05 13:36:54', 'Admin', '超级管理员', '博客管理', '修改文章:Vue.js vs React vs Angular 2025年对比分析');
INSERT INTO `base_userlog` VALUES ('1963840361168113664', '2025-09-05 13:43:23', 'Admin', '超级管理员', '博客管理', '修改项目:个人博客系统');
INSERT INTO `base_userlog` VALUES ('1963840436690751488', '2025-09-05 13:43:41', 'Admin', '超级管理员', '博客管理', '修改项目:个人博客系统');
INSERT INTO `base_userlog` VALUES ('1963840587509534720', '2025-09-05 13:44:17', 'Admin', '超级管理员', '博客管理', '添加项目:考试系统');
INSERT INTO `base_userlog` VALUES ('1963840788236341248', '2025-09-05 13:45:05', 'Admin', '超级管理员', '博客管理', '修改项目:考试系统');
INSERT INTO `base_userlog` VALUES ('1963840837968203776', '2025-09-05 13:45:17', 'Admin', '超级管理员', '博客管理', '修改项目:考试系统');
INSERT INTO `base_userlog` VALUES ('1963841169234333696', '2025-09-05 13:46:36', 'Admin', '超级管理员', '博客管理', '添加项目:fsefe');
INSERT INTO `base_userlog` VALUES ('1963859606711046144', '2025-09-05 14:59:52', 'Admin', '超级管理员', '博客管理', '修改项目:个人博客系统');
INSERT INTO `base_userlog` VALUES ('1963861358701187072', '2025-09-05 15:06:49', 'Admin', '超级管理员', '博客管理', '修改项目:个人博客系统');
INSERT INTO `base_userlog` VALUES ('1963862082940047360', '2025-09-05 15:09:42', 'Admin', '超级管理员', '博客管理', '修改项目:个人博客系统');
INSERT INTO `base_userlog` VALUES ('1963862207699619840', '2025-09-05 15:10:12', 'Admin', '超级管理员', '博客管理', '修改项目:在线代码编辑器');
INSERT INTO `base_userlog` VALUES ('1963862232278241280', '2025-09-05 15:10:18', 'Admin', '超级管理员', '博客管理', '修改项目:个人博客系统');
INSERT INTO `base_userlog` VALUES ('1963862265249665024', '2025-09-05 15:10:25', 'Admin', '超级管理员', '博客管理', '修改项目:任务管理系统');
INSERT INTO `base_userlog` VALUES ('1963862746835456000', '2025-09-05 15:12:20', 'Admin', '超级管理员', '博客管理', '添加项目:000');
INSERT INTO `base_userlog` VALUES ('1963862955720183808', '2025-09-05 15:13:10', 'Admin', '超级管理员', '博客管理', '修改项目:个人博客系统');
INSERT INTO `base_userlog` VALUES ('1963863036255014912', '2025-09-05 15:13:29', 'Admin', '超级管理员', '博客管理', '修改项目:个人博客系统');
INSERT INTO `base_userlog` VALUES ('1963863952806907904', '2025-09-05 15:17:08', 'Admin', '超级管理员', '博客管理', '修改项目:个人博客系统');
INSERT INTO `base_userlog` VALUES ('1963864387483602944', '2025-09-05 15:18:51', 'Admin', '超级管理员', '博客管理', '修改项目:个人博客系统');
INSERT INTO `base_userlog` VALUES ('1963864759463841792', '2025-09-05 15:20:20', 'Admin', '超级管理员', '博客管理', '修改项目:任务管理系统');
INSERT INTO `base_userlog` VALUES ('1963864789222428672', '2025-09-05 15:20:27', 'Admin', '超级管理员', '博客管理', '修改项目:在线代码编辑器');
INSERT INTO `base_userlog` VALUES ('1963864860659814400', '2025-09-05 15:20:44', 'Admin', '超级管理员', '博客管理', '修改项目:在线代码编辑器');
INSERT INTO `base_userlog` VALUES ('1963865309299347456', '2025-09-05 15:22:31', 'Admin', '超级管理员', '博客管理', '修改项目:在线代码编辑器');
INSERT INTO `base_userlog` VALUES ('1963865477553852416', '2025-09-05 15:23:11', 'Admin', '超级管理员', '博客管理', '修改项目:在线代码编辑器');
INSERT INTO `base_userlog` VALUES ('1963866123191455744', '2025-09-05 15:25:45', 'Admin', '超级管理员', '博客管理', '修改项目:个人博客系统');
INSERT INTO `base_userlog` VALUES ('1963866200949657600', '2025-09-05 15:26:04', 'Admin', '超级管理员', '博客管理', '修改项目:个人博客系统');
INSERT INTO `base_userlog` VALUES ('1963866590273343488', '2025-09-05 15:27:37', 'Admin', '超级管理员', '博客管理', '修改文章:Flutter跨平台开发入门实战');
INSERT INTO `base_userlog` VALUES ('1963866902157594624', '2025-09-05 15:28:51', 'Admin', '超级管理员', '博客管理', '添加文章:fff');
INSERT INTO `base_userlog` VALUES ('1963866965617414144', '2025-09-05 15:29:06', 'Admin', '超级管理员', '博客管理', '修改文章:fff');
INSERT INTO `base_userlog` VALUES ('1963867475414093824', '2025-09-05 15:31:08', 'Admin', '超级管理员', '博客管理', '添加文章:fsf');
INSERT INTO `base_userlog` VALUES ('1963870474257567744', '2025-09-05 15:43:03', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1963870578125312000', '2025-09-05 15:43:27', 'Admin', '超级管理员', '博客管理', '修改文章:使用Node.js构建RESTful API最佳实践');
INSERT INTO `base_userlog` VALUES ('1963870779414155264', '2025-09-05 15:44:15', 'Admin', '超级管理员', '博客管理', '修改项目:任务管理系统');
INSERT INTO `base_userlog` VALUES ('1963871299549794304', '2025-09-05 15:46:19', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1963871438288982016', '2025-09-05 15:46:53', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1963872725642514432', '2025-09-05 15:51:59', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1963872894236758016', '2025-09-05 15:52:40', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1963872913136291840', '2025-09-05 15:52:44', 'Admin', '超级管理员', '博客管理', '修改文章:使用Node.js构建RESTful API最佳实践');
INSERT INTO `base_userlog` VALUES ('1963873137573498880', '2025-09-05 15:53:38', 'Admin', '超级管理员', '博客管理', '修改文章:使用Node.js构建RESTful API最佳实践');
INSERT INTO `base_userlog` VALUES ('1963873446676926464', '2025-09-05 15:54:51', 'Admin', '超级管理员', '博客管理', '修改文章:React Hooks深度解析');
INSERT INTO `base_userlog` VALUES ('1963873485994332160', '2025-09-05 15:55:01', 'Admin', '超级管理员', '博客管理', '修改文章:React Hooks深度解析');
INSERT INTO `base_userlog` VALUES ('1963873532005847040', '2025-09-05 15:55:12', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1963873598791749632', '2025-09-05 15:55:28', 'Admin', '超级管理员', '博客管理', '修改项目:个人博客系统');
INSERT INTO `base_userlog` VALUES ('1963906045231239168', '2025-09-05 18:04:23', 'Admin', '超级管理员', '系统用户管理', '添加用户:ff');
INSERT INTO `base_userlog` VALUES ('1963906045352873984', '2025-09-05 18:04:23', 'Admin', '超级管理员', '系统用户管理', '添加用户:ff');
INSERT INTO `base_userlog` VALUES ('1964931858248503296', '2025-09-08 14:00:36', 'Admin', '超级管理员', '博客管理', '修改项目:个人博客系统');
INSERT INTO `base_userlog` VALUES ('1964940676214820864', '2025-09-08 14:35:39', 'Admin', '超级管理员', '博客管理', '添加文章:测试');
INSERT INTO `base_userlog` VALUES ('1964942467732082688', '2025-09-08 14:42:46', 'Admin', '超级管理员', '博客管理', '修改文章:Vue.js vs React vs Angular 2025年对比分析');
INSERT INTO `base_userlog` VALUES ('1964958395807043584', '2025-09-08 15:46:03', 'Admin', '超级管理员', '系统用户管理', '修改用户:超级管理员');
INSERT INTO `base_userlog` VALUES ('1964958395878346752', '2025-09-08 15:46:03', 'Admin', '超级管理员', '系统用户管理', '修改用户:超级管理员');
INSERT INTO `base_userlog` VALUES ('1964958411594403840', '2025-09-08 15:46:07', 'Admin', '超级管理员', '系统用户管理', '修改用户:ff');
INSERT INTO `base_userlog` VALUES ('1964958411644735488', '2025-09-08 15:46:07', 'Admin', '超级管理员', '系统用户管理', '修改用户:ff');
INSERT INTO `base_userlog` VALUES ('1964958516691079168', '2025-09-08 15:46:32', 'Admin', '超级管理员', '系统角色管理', '修改角色:部门管理员');
INSERT INTO `base_userlog` VALUES ('1964958516766576640', '2025-09-08 15:46:32', 'Admin', '超级管理员', '系统角色管理', '修改角色:部门管理员');
INSERT INTO `base_userlog` VALUES ('1964958559846273024', '2025-09-08 15:46:42', 'Admin', '超级管理员', '系统角色管理', '修改角色:用户');
INSERT INTO `base_userlog` VALUES ('1964958559896604672', '2025-09-08 15:46:43', 'Admin', '超级管理员', '系统角色管理', '修改角色:用户');
INSERT INTO `base_userlog` VALUES ('1964958591462936576', '2025-09-08 15:46:50', 'Admin', '超级管理员', '系统用户管理', '修改用户:ff');
INSERT INTO `base_userlog` VALUES ('1964958591509073920', '2025-09-08 15:46:50', 'Admin', '超级管理员', '系统用户管理', '修改用户:ff');
INSERT INTO `base_userlog` VALUES ('1965230564483010560', '2025-09-09 09:47:33', 'Admin', '超级管理员', '博客管理', '修改项目:个人博客系统');
INSERT INTO `base_userlog` VALUES ('1965230580912099328', '2025-09-09 09:47:37', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1965309921117343744', '2025-09-09 15:02:54', 'Admin', '超级管理员', '博客管理', '添加文章:方法');
INSERT INTO `base_userlog` VALUES ('1965338438588698624', '2025-09-09 16:56:13', 'Admin', '超级管理员', '博客管理', '修改文章:React Hooks深度解析');
INSERT INTO `base_userlog` VALUES ('1965338474391277568', '2025-09-09 16:56:21', 'Admin', '超级管理员', '博客管理', '修改文章:Spring Boot微服务架构实践');
INSERT INTO `base_userlog` VALUES ('1965338592238637056', '2025-09-09 16:56:49', 'Admin', '超级管理员', '博客管理', '修改文章:Spring Boot微服务架构实践');
INSERT INTO `base_userlog` VALUES ('1965338706885742592', '2025-09-09 16:57:17', 'Admin', '超级管理员', '博客管理', '修改文章:Spring Boot微服务架构实践');
INSERT INTO `base_userlog` VALUES ('1965338900721307648', '2025-09-09 16:58:03', 'Admin', '超级管理员', '博客管理', '修改文章:Spring Boot微服务架构实践');
INSERT INTO `base_userlog` VALUES ('1965339571839307776', '2025-09-09 17:00:43', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1965339643557711872', '2025-09-09 17:01:00', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1965342471252611072', '2025-09-09 17:12:14', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1965343334910136320', '2025-09-09 17:15:40', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1965343707800539136', '2025-09-09 17:17:09', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1965343743955439616', '2025-09-09 17:17:18', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1965344190950805504', '2025-09-09 17:19:04', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1965344593490743296', '2025-09-09 17:20:40', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1965344836697460736', '2025-09-09 17:21:38', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1965345102939295744', '2025-09-09 17:22:42', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1965345751278030848', '2025-09-09 17:25:16', 'Admin', '超级管理员', '博客管理', '修改文章:使用Node.js构建RESTful API最佳实践');
INSERT INTO `base_userlog` VALUES ('1965345781565100032', '2025-09-09 17:25:23', 'Admin', '超级管理员', '博客管理', '修改文章:React Hooks深度解析');
INSERT INTO `base_userlog` VALUES ('1965346536250413056', '2025-09-09 17:28:23', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1965348503123136512', '2025-09-09 17:36:12', 'Admin', '超级管理员', '博客管理', '修改项目:个人博客系统');
INSERT INTO `base_userlog` VALUES ('1965617904397324288', '2025-09-10 11:26:42', 'Admin', '超级管理员', '系统用户管理', '删除用户:aaa');
INSERT INTO `base_userlog` VALUES ('1965617904716091392', '2025-09-10 11:26:43', 'Admin', '超级管理员', '系统用户管理', '删除用户:aaa');
INSERT INTO `base_userlog` VALUES ('1965617964816273408', '2025-09-10 11:26:57', 'Admin', '超级管理员', '部门管理', '删除部门名:鄞州事业部');
INSERT INTO `base_userlog` VALUES ('1965617971707514880', '2025-09-10 11:26:59', 'Admin', '超级管理员', '部门管理', '删除部门名:海曙事业部');
INSERT INTO `base_userlog` VALUES ('1965617978414206976', '2025-09-10 11:27:00', 'Admin', '超级管理员', '部门管理', '删除部门名:宁波分公司');
INSERT INTO `base_userlog` VALUES ('1965630539796844544', '2025-09-10 12:16:55', 'Admin', '超级管理员', '博客管理', '修改项目:任务管理系统');
INSERT INTO `base_userlog` VALUES ('1965630574408241152', '2025-09-10 12:17:03', 'Admin', '超级管理员', '博客管理', '修改项目:在线代码编辑器');
INSERT INTO `base_userlog` VALUES ('1966416237055447040', '2025-09-12 16:19:00', 'Admin', '超级管理员', '博客管理', '修改项目:个人博客系统');
INSERT INTO `base_userlog` VALUES ('1966417437872099328', '2025-09-12 16:23:46', 'Admin', '超级管理员', '博客管理', '修改文章:Flutter跨平台开发入门实战');
INSERT INTO `base_userlog` VALUES ('1968242470634393600', '2025-09-17 17:15:48', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1968242531208531968', '2025-09-17 17:16:02', 'Admin', '超级管理员', '博客管理', '修改文章:深入理解Vue.js响应式系统');
INSERT INTO `base_userlog` VALUES ('1968936722758635520', '2025-09-19 15:14:30', 'Admin', '超级管理员', '系统角色管理', '修改角色:超级管理员');
INSERT INTO `base_userlog` VALUES ('1968936722871881728', '2025-09-19 15:14:30', 'Admin', '超级管理员', '系统角色管理', '修改角色:超级管理员');
INSERT INTO `base_userlog` VALUES ('1968936800604917760', '2025-09-19 15:14:49', 'Admin', '超级管理员', '系统用户管理', '删除用户:user');
INSERT INTO `base_userlog` VALUES ('1968936800709775360', '2025-09-19 15:14:49', 'Admin', '超级管理员', '系统用户管理', '删除用户:user');
INSERT INTO `base_userlog` VALUES ('1968936813011668992', '2025-09-19 15:14:52', 'Admin', '超级管理员', '系统用户管理', '删除用户:ffff');
INSERT INTO `base_userlog` VALUES ('1968936813057806336', '2025-09-19 15:14:52', 'Admin', '超级管理员', '系统用户管理', '删除用户:ffff');
INSERT INTO `base_userlog` VALUES ('1968936821853261824', '2025-09-19 15:14:54', 'Admin', '超级管理员', '系统用户管理', '删除用户:ff');
INSERT INTO `base_userlog` VALUES ('1968936821920370688', '2025-09-19 15:14:54', 'Admin', '超级管理员', '系统用户管理', '删除用户:ff');
INSERT INTO `base_userlog` VALUES ('1968936905118584832', '2025-09-19 15:15:14', 'Admin', '超级管理员', '系统用户管理', '修改用户:花');
INSERT INTO `base_userlog` VALUES ('1968936905324105728', '2025-09-19 15:15:14', 'Admin', '超级管理员', '系统用户管理', '修改用户:花');
INSERT INTO `base_userlog` VALUES ('1968937075977752576', '2025-09-19 15:15:55', 'Admin', '超级管理员', '系统角色管理', '修改角色:超级管理员');
INSERT INTO `base_userlog` VALUES ('1968937076057444352', '2025-09-19 15:15:55', 'Admin', '超级管理员', '系统角色管理', '修改角色:超级管理员');
INSERT INTO `base_userlog` VALUES ('1968955593469202432', '2025-09-19 16:29:30', 'Admin', '超级管理员', '博客管理', '添加文章:非法额阿尔');
INSERT INTO `base_userlog` VALUES ('1968955674087919616', '2025-09-19 16:29:49', 'Admin', '超级管理员', '博客管理', '修改文章:非法额阿尔');
INSERT INTO `base_userlog` VALUES ('1968955701342507008', '2025-09-19 16:29:55', 'Admin', '超级管理员', '博客管理', '修改文章:非法额阿尔');
INSERT INTO `base_userlog` VALUES ('1968955810562183168', '2025-09-19 16:30:21', 'Admin', '超级管理员', '博客管理', '修改文章:非法额阿尔');
INSERT INTO `base_userlog` VALUES ('1968968068369485824', '2025-09-19 17:19:04', 'Admin', '超级管理员', '系统用户管理', '添加用户:r');
INSERT INTO `base_userlog` VALUES ('1968968068478537728', '2025-09-19 17:19:04', 'Admin', '超级管理员', '系统用户管理', '添加用户:r');

-- ----------------------------
-- Table structure for base_userrole
-- ----------------------------
DROP TABLE IF EXISTS `base_userrole`;
CREATE TABLE `base_userrole`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '主键',
  `CreateTime` datetime NOT NULL COMMENT '创建时间',
  `CreatorId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人Id',
  `Deleted` tinyint(4) NOT NULL DEFAULT 0 COMMENT '否已删除',
  `UserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户Id',
  `RoleId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '角色Id',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '用户角色表' ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of base_userrole
-- ----------------------------
INSERT INTO `base_userrole` VALUES ('1181927367719784448', '2019-10-09 21:40:18', NULL, 0, '1181922344629702656', '1180819481383931904');
INSERT INTO `base_userrole` VALUES ('1181927367719784449', '2019-10-09 21:40:18', NULL, 0, '1181922344629702656', '1180486275199668224');
INSERT INTO `base_userrole` VALUES ('1181927783786352640', '2019-10-09 21:41:57', NULL, 0, '1181927783727632384', '1180819481383931904');
INSERT INTO `base_userrole` VALUES ('1964958395710574592', '2025-09-08 15:46:03', NULL, 0, 'Admin', '1180486275199668224');
INSERT INTO `base_userrole` VALUES ('1964958591429382144', '2025-09-08 15:46:50', NULL, 0, '1963906044644036608', '1180819481383931904');
INSERT INTO `base_userrole` VALUES ('1968968068302376960', '2025-09-19 17:19:04', NULL, 0, '1968968067677425664', '1180486275199668224');

-- ----------------------------
-- Table structure for blog_article_categories
-- ----------------------------
DROP TABLE IF EXISTS `blog_article_categories`;
CREATE TABLE `blog_article_categories`  (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '分类ID，主键',
  `name` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT '分类名称',
  `description` text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT '分类描述',
  `sort_order` int(11) NULL DEFAULT 0 COMMENT '排序权重，数字越大越靠前',
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 6 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci COMMENT = '博客文章分类表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of blog_article_categories
-- ----------------------------
INSERT INTO `blog_article_categories` VALUES (1, '技术分享', '分享各种编程技术和开发经验', 10, '2025-09-05 10:00:00');
INSERT INTO `blog_article_categories` VALUES (2, '项目实践', '记录项目开发过程和经验总结', 20, '2025-09-05 10:00:00');
INSERT INTO `blog_article_categories` VALUES (3, '工具推荐', '推荐实用的开发工具和软件', 15, '2025-09-05 10:00:00');

-- ----------------------------
-- Table structure for blog_articles
-- ----------------------------
DROP TABLE IF EXISTS `blog_articles`;
CREATE TABLE `blog_articles`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `title` varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT '文章标题',
  `summary` text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT '文章摘要/简介',
  `content` longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT '文章内容，富文本',
  `cover_image` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT '封面图片URL',
  `category_id` int(11) NULL DEFAULT NULL COMMENT '文章分类ID',
  `status` enum('draft','published','hidden') CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT 'draft' COMMENT '文章状态：草稿/已发布/隐藏',
  `is_featured` tinyint(4) NULL DEFAULT 0 COMMENT '是否精选文章：1是/0否',
  `view_count` int(11) NULL DEFAULT 0 COMMENT '浏览次数',
  `like_count` int(11) NULL DEFAULT 0 COMMENT '点赞数量',
  `comment_count` int(11) NULL DEFAULT 0 COMMENT '评论数量',
  `published_at` timestamp NULL DEFAULT NULL COMMENT '发布时间',
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  `author_id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '作者ID，关联Base_User表',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_articles_status`(`status`) USING BTREE,
  INDEX `idx_articles_featured`(`is_featured`) USING BTREE,
  INDEX `idx_status_featured`(`status`, `is_featured`) USING BTREE,
  INDEX `idx_category_status`(`category_id`, `status`) USING BTREE,
  INDEX `idx_published_time`(`published_at`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 30 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci COMMENT = '博客文章表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of blog_articles
-- ----------------------------
INSERT INTO `blog_articles` VALUES (1, '深入理解Vue.js响应式系统', 'Vue.js的响应式系统是其核心特性之一，本文将深入解析其实现原理', '<p>反反复复凤飞飞反反复复发发发发发发发发发发发发发发发发发发发</p>', 'http://192.168.124.23:5000/Upload/2025/09/09/eca3d0a0faf140e691dedcfb0920f00e.jpg', 1, 'published', 1, 156, 1, 4, '2025-09-05 10:30:00', '2025-09-05 10:20:00', '2025-09-19 16:51:38', 'Admin');
INSERT INTO `blog_articles` VALUES (2, '使用Node.js构建RESTful API最佳实践', '分享在Node.js中构建RESTful API的经验和最佳实践', '<h1>Node.js RESTful API最佳实践</h1><p>构建一个高质量的RESTful API需要遵循一些最佳实践...</p><h2>项目结构</h2><p>良好的项目结构是成功项目的基础：</p><pre><code>project/\n├── controllers/\n├── models/\n├── routes/\n├── middleware/\n├── utils/\n└── app.js\n</code></pre><h2>错误处理</h2><p>统一的错误处理机制非常重要：</p><pre><code>// 全局错误处理中间件\napp.use((err, req, res, next) =&gt; {\n  res.status(err.status || 500).json({\n    message: err.message,\n    stack: process.env.NODE_ENV === \'development\' ? err.stack : {}\n  })\n})\n</code></pre>', 'http://192.168.124.23:5000/Upload/2025/09/09/eca3d0a0faf140e691dedcfb0920f00e.jpg', 1, 'published', 1, 89, 2, 10, '2025-09-05 11:00:00', '2025-09-05 10:50:00', '2025-09-19 18:04:16', 'Admin');
INSERT INTO `blog_articles` VALUES (7, 'Docker容器化部署完整指南', '从Docker基础到生产环境部署的完整实战教程', '<h1>Docker容器化部署完整指南</h1>\r\n<p>Docker已经成为现代应用部署的标准工具，本文将带你从零开始掌握Docker容器化部署。</p>\r\n<h2>Docker基础概念</h2>\r\n<ul>\r\n<li><strong>镜像(Image)</strong>：只读的模板</li>\r\n<li><strong>容器(Container)</strong>：镜像的运行实例</li>\r\n<li><strong>仓库(Repository)</strong>：存储镜像的地方</li>\r\n</ul>\r\n<h2>Dockerfile最佳实践</h2>\r\n<pre><code># 使用多阶段构建\r\nFROM node:16-alpine AS builder\r\nWORKDIR /app\r\nCOPY package*.json ./\r\nRUN npm ci --only=production\r\n\r\nFROM nginx:alpine\r\nCOPY --from=builder /app/dist /usr/share/nginx/html\r\nEXPOSE 80\r\nCMD [\"nginx\", \"-g\", \"daemon off;\"]</code></pre>\r\n<h2>Docker Compose编排</h2>\r\n<pre><code>version: \"3.8\"\r\nservices:\r\n  web:\r\n    build: .\r\n    ports:\r\n      - \"3000:3000\"\r\n    environment:\r\n      - NODE_ENV=production\r\n  db:\r\n    image: mysql:8.0\r\n    environment:\r\n      - MYSQL_ROOT_PASSWORD=password</code></pre>\r\n<h2>生产环境部署</h2>\r\n<p>在生产环境中，我们需要考虑容器编排、负载均衡、监控等问题。</p>', 'http://192.168.124.23:5000/Upload/2025/09/09/eca3d0a0faf140e691dedcfb0920f00e.jpg', 3, 'published', 1, 298, 1, 0, '2025-09-05 17:00:00', '2025-09-05 16:30:00', '2025-09-19 18:02:36', 'Admin');
INSERT INTO `blog_articles` VALUES (8, 'Vue.js vs React vs Angular 2025年对比分析', '三大前端框架的深度对比分析，帮你选择最适合的技术栈', '<h1>Vue.js vs React vs Angular 2025年对比分析</h1>\n<p>前端框架的选择往往决定了项目的技术方向。本文将从多个维度对比Vue.js、React和Angular。</p>\n<h2>学习曲线对比</h2>\n<table>\n<tbody><tr><th>框架</th><th>学习难度</th><th>上手时间</th><th>适合人群</th></tr>\n<tr><td>Vue.js</td><td>易</td><td>1-2周</td><td>初学者、快速开发</td></tr>\n<tr><td>React</td><td>中</td><td>2-4周</td><td>有JS基础的开发者</td></tr>\n<tr><td>Angular</td><td>难</td><td>1-2月</td><td>大型项目、企业级开发</td></tr>\n</tbody></table>\n<h2>性能对比</h2>\n<ul>\n<li><strong>Vue.js</strong>：轻量级，虚拟DOM，响应式系统优秀</li>\n<li><strong>React</strong>：虚拟DOM，Fiber架构，性能优异</li>\n<li><strong>Angular</strong>：变更检测机制，大型应用性能稳定</li>\n</ul>\n<h2>生态系统</h2>\n<h3>Vue.js生态</h3>\n<ul>\n<li>Vue Router：路由管理</li>\n<li>Vuex/Pinia：状态管理</li>\n<li>Nuxt.js：服务端渲染</li>\n</ul>\n<h3>React生态</h3>\n<ul>\n<li>React Router：路由管理</li>\n<li>Redux/Zustand：状态管理</li>\n<li>Next.js：全栈框架</li>\n</ul>\n<h2>选择建议</h2>\n<p>根据项目需求和团队情况选择合适的框架是关键。</p>', 'http://192.168.124.23:5000/Upload/2025/09/09/eca3d0a0faf140e691dedcfb0920f00e.jpg', 2, 'published', 1, 445, 0, 0, '2025-09-05 18:00:00', '2025-09-05 17:45:00', '2025-09-19 16:51:38', 'Admin');
INSERT INTO `blog_articles` VALUES (9, 'ChatGPT在软件开发中的应用实践', '探索AI助手如何改变传统的软件开发流程', '<h1>ChatGPT在软件开发中的应用实践</h1>\r\n<p>AI技术的快速发展正在深刻改变软件开发行业，ChatGPT等AI助手已成为开发者的得力工具。</p>\r\n<h2>代码生成与补全</h2>\r\n<p>ChatGPT可以根据自然语言描述生成代码：</p>\r\n<blockquote>\r\n<p>提示：请帮我写一个JavaScript函数，用于计算数组中所有数字的平均值</p>\r\n</blockquote>\r\n<pre><code>function calculateAverage(numbers) {\r\n    if (numbers.length === 0) return 0;\r\n    const sum = numbers.reduce((acc, num) => acc + num, 0);\r\n    return sum / numbers.length;\r\n}</code></pre>\r\n<h2>代码审查与优化</h2>\r\n<p>AI可以帮助发现代码中的潜在问题：</p>\r\n<ul>\r\n<li>性能瓶颈识别</li>\r\n<li>安全漏洞检测</li>\r\n<li>代码规范检查</li>\r\n<li>重构建议</li>\r\n</ul>\r\n<h2>测试用例生成</h2>\r\n<p>AI可以自动生成单元测试：</p>\r\n<pre><code>describe(\"calculateAverage\", () => {\r\n    test(\"should return 0 for empty array\", () => {\r\n        expect(calculateAverage([])).toBe(0);\r\n    });\r\n    \r\n    test(\"should calculate correct average\", () => {\r\n        expect(calculateAverage([1, 2, 3, 4, 5])).toBe(3);\r\n    });\r\n});</code></pre>\r\n<h2>文档编写</h2>\r\n<p>AI可以帮助生成技术文档、API文档等。</p>\r\n<h2>使用建议</h2>\r\n<ul>\r\n<li>验证AI生成的代码</li>\r\n<li>理解代码逻辑</li>\r\n<li>结合人工审查</li>\r\n<li>保持学习态度</li>\r\n</ul>', 'http://192.168.124.23:5000/Upload/2025/09/09/eca3d0a0faf140e691dedcfb0920f00e.jpg', 1, 'published', 1, 367, 0, 0, '2025-09-05 19:00:00', '2025-09-05 18:30:00', '2025-09-19 16:51:38', 'Admin');
INSERT INTO `blog_articles` VALUES (6, 'Redis缓存策略与最佳实践', '深入探讨Redis在项目中的缓存策略和性能优化技巧', '<h1>Redis缓存策略与最佳实践</h1>\n<p>Redis作为内存数据库，在缓存场景中有着广泛的应用。合理的缓存策略能显著提升系统性能。</p>\n<h2>常见缓存模式</h2>\n<h3>1. Cache-Aside（旁路缓存）</h3>\n<ul>\n<li>读取：先查缓存，缓存没有再查数据库</li>\n<li>写入：先写数据库，再删除缓存</li>\n</ul>\n<h3>2. Write-Through（写穿透）</h3>\n<p>同时写入缓存和数据库，保证数据一致性。</p>\n<h3>3. Write-Behind（写回）</h3>\n<p>只写缓存，异步批量写入数据库。</p>\n<h2>缓存策略实现</h2>\n<pre><code>@Service\npublic class UserService {\n    @Cacheable(value = \"users\", key = \"#id\")\n    public User getUserById(Long id) {\n        return userRepository.findById(id);\n    }\n    \n    @CacheEvict(value = \"users\", key = \"#user.id\")\n    public void updateUser(User user) {\n        userRepository.save(user);\n    }\n}</code></pre>\n<h2>性能优化技巧</h2>\n<ul>\n<li>合理设置过期时间</li>\n<li>使用Pipeline批量操作</li>\n<li>选择合适的数据结构</li>\n<li>监控内存使用情况</li>\n</ul>', 'http://192.168.124.23:5000/Upload/2025/09/09/eca3d0a0faf140e691dedcfb0920f00e.jpg', 1, 'published', 0, 156, 0, 2, '2025-09-05 16:00:00', '2025-09-05 15:45:00', '2025-09-19 16:51:38', 'Admin');
INSERT INTO `blog_articles` VALUES (5, 'Spring Boot微服务架构实践', '基于Spring Boot构建微服务架构的完整实践指南', '<h1>Spring Boot微服务架构实践</h1>\n<p>微服务架构是当前企业级应用开发的主流架构模式，Spring Boot为微服务开发提供了强大的支持。</p>\n<h2>微服务架构优势</h2>\n<ul>\n<li>服务独立部署</li>\n<li>技术栈多样化</li>\n<li>团队开发效率高</li>\n<li>容错性强</li>\n</ul>\n<h2>核心组件</h2>\n<h3>1. 服务注册与发现</h3>\n<pre><code>@SpringBootApplication\n@EnableEurekaClient\npublic class UserServiceApplication {\n    public static void main(String[] args) {\n        SpringApplication.run(UserServiceApplication.class, args);\n    }\n}</code></pre>\n<h3>2. 服务网关</h3>\n<p>使用Spring Cloud Gateway实现统一的API网关，提供路由、限流、安全等功能。</p>\n<h3>3. 配置中心</h3>\n<p>通过Spring Cloud Config实现集中化的配置管理。</p>', 'http://192.168.124.23:5000/Upload/2025/09/09/eca3d0a0faf140e691dedcfb0920f00e.jpg', 1, 'published', 1, 187, 0, 1, '2025-09-05 15:00:00', '2025-09-05 14:30:00', '2025-09-22 10:54:38', 'Admin');
INSERT INTO `blog_articles` VALUES (4, 'React Hooks深度解析', 'React Hooks是React 16.8引入的新特性，让我们可以在函数组件中使用state和其他React特性', '<h1>React Hooks深度解析</h1>\n<p>React Hooks是React 16.8版本引入的革命性特性，它改变了我们编写React组件的方式。</p>\n<h2>什么是Hooks</h2>\n<p>Hooks是一些可以让你在函数组件里\"钩入\"React state及生命周期等特性的函数。</p>\n<h2>常用的Hooks</h2>\n<ul>\n<li><strong>useState</strong>：用于在函数组件中添加state</li>\n<li><strong>useEffect</strong>：用于执行副作用操作</li>\n<li><strong>useContext</strong>：用于订阅React context</li>\n<li><strong>useReducer</strong>：用于管理复杂的state逻辑</li>\n</ul>\n<h2>自定义Hooks</h2>\n<pre><code>function useCounter(initialValue = 0) {\n  const [count, setCount] = useState(initialValue);\n  \n  const increment = () =&gt; setCount(count + 1);\n  const decrement = () =&gt; setCount(count - 1);\n  \n  return { count, increment, decrement };\n}</code></pre>\n<p>自定义Hooks让我们可以提取组件逻辑到可复用的函数中。</p>', 'http://192.168.124.23:5000/Upload/2025/09/09/eca3d0a0faf140e691dedcfb0920f00e.jpg', 1, 'published', 1, 234, 0, 0, '2025-09-05 14:00:00', '2025-09-05 13:45:00', '2025-09-19 16:51:38', 'Admin');
INSERT INTO `blog_articles` VALUES (10, 'Web应用安全防护实战指南', '全面介绍Web应用常见安全威胁及防护措施', '<h1>Web应用安全防护实战指南</h1>\r\n<p>随着Web应用的普及，安全问题日益突出。本文将介绍常见的Web安全威胁及相应的防护措施。</p>\r\n<h2>OWASP Top 10 安全风险</h2>\r\n<ol>\r\n<li><strong>注入攻击</strong>：SQL注入、NoSQL注入等</li>\r\n<li><strong>失效的身份认证</strong>：会话管理漏洞</li>\r\n<li><strong>敏感数据泄露</strong>：数据传输和存储安全</li>\r\n<li><strong>XML外部实体注入(XXE)</strong></li>\r\n<li><strong>失效的访问控制</strong></li>\r\n</ol>\r\n<h2>SQL注入防护</h2>\r\n<pre><code>// 错误示例 - 容易受到SQL注入攻击\r\nString sql = \"SELECT * FROM users WHERE id = \" + userId;\r\n\r\n// 正确示例 - 使用参数化查询\r\nString sql = \"SELECT * FROM users WHERE id = ?\";\r\nPreparedStatement stmt = connection.prepareStatement(sql);\r\nstmt.setInt(1, userId);</code></pre>\r\n<h2>XSS攻击防护</h2>\r\n<ul>\r\n<li>输入验证和过滤</li>\r\n<li>输出编码</li>\r\n<li>Content Security Policy(CSP)</li>\r\n<li>HttpOnly Cookie</li>\r\n</ul>\r\n<h2>CSRF攻击防护</h2>\r\n<pre><code>// 使用CSRF Token\r\n&lt;form action=\"/transfer\" method=\"post\"&gt;\r\n    &lt;input type=\"hidden\" name=\"csrf_token\" value=\"${csrfToken}\"&gt;\r\n    &lt;input type=\"text\" name=\"amount\"&gt;\r\n    &lt;button type=\"submit\"&gt;转账&lt;/button&gt;\r\n&lt;/form&gt;</code></pre>\r\n<h2>安全开发最佳实践</h2>\r\n<ul>\r\n<li>输入验证和输出编码</li>\r\n<li>最小权限原则</li>\r\n<li>定期安全审计</li>\r\n<li>安全培训</li>\r\n</ul>', 'http://192.168.124.23:5000/Upload/2025/09/09/eca3d0a0faf140e691dedcfb0920f00e.jpg', 1, 'published', 0, 289, 0, 5, '2025-09-05 20:00:00', '2025-09-05 19:45:00', '2025-09-19 16:51:38', 'Admin');
INSERT INTO `blog_articles` VALUES (11, 'Flutter跨平台开发入门实战', '使用Flutter开发跨平台移动应用的完整教程', '<h1>Flutter跨平台开发入门实战</h1>\n<p>Flutter是Google推出的跨平台移动应用开发框架，使用Dart语言，可以同时开发iOS和Android应用。</p>\n<h2>Flutter优势</h2>\n<ul>\n<li><strong>跨平台</strong>：一套代码运行多个平台</li>\n<li><strong>高性能</strong>：直接编译为原生代码</li>\n<li><strong>热重载</strong>：快速开发和调试</li>\n<li><strong>丰富的UI组件</strong>：Material Design和Cupertino</li>\n</ul>\n<h2>开发环境搭建</h2>\n<ol>\n<li>下载Flutter SDK</li>\n<li>配置环境变量</li>\n<li>安装Android Studio/VS Code</li>\n<li>配置模拟器或真机</li>\n</ol>\n<h2>第一个Flutter应用</h2>\n<pre><code>import \"package:flutter/material.dart\";\n\nvoid main() {\n  runApp(MyApp());\n}\n\nclass MyApp extends StatelessWidget {\n  @override\n  Widget build(BuildContext context) {\n    return MaterialApp(\n      title: \"Flutter Demo\",\n      home: Scaffold(\n        appBar: AppBar(\n          title: Text(\"Hello Flutter\"),\n        ),\n        body: Center(\n          child: Text(\"Hello, World!\"),\n        ),\n      ),\n    );\n  }\n}</code></pre>\n<h2>常用Widget</h2>\n<ul>\n<li><strong>Container</strong>：容器组件</li>\n<li><strong>Row/Column</strong>：布局组件</li>\n<li><strong>ListView</strong>：列表组件</li>\n<li><strong>TextField</strong>：输入框组件</li>\n</ul>\n<h2>状态管理</h2>\n<p>Flutter提供多种状态管理方案：</p>\n<ul>\n<li>setState（简单状态）</li>\n<li>Provider（中等复杂度）</li>\n<li>Bloc/Cubit（复杂状态）</li>\n<li>Riverpod（新一代状态管理）</li>\n</ul>', 'http://192.168.124.23:5000/Upload/2025/09/09/eca3d0a0faf140e691dedcfb0920f00e.jpg', 1, 'published', 0, 0, 0, 0, NULL, '2025-09-05 20:15:00', '2025-09-19 16:10:09', 'Admin');
INSERT INTO `blog_articles` VALUES (12, 'AWS云服务架构设计与实践', '基于AWS构建可扩展、高可用的云原生应用架构', '<h1>AWS云服务架构设计与实践</h1>\r\n<p>Amazon Web Services (AWS)是全球领先的云计算平台，提供了丰富的云服务来构建现代化应用。</p>\r\n<h2>核心服务介绍</h2>\r\n<h3>计算服务</h3>\r\n<ul>\r\n<li><strong>EC2</strong>：弹性计算云</li>\r\n<li><strong>Lambda</strong>：无服务器计算</li>\r\n<li><strong>ECS/EKS</strong>：容器服务</li>\r\n<li><strong>Fargate</strong>：无服务器容器</li>\r\n</ul>\r\n<h3>存储服务</h3>\r\n<ul>\r\n<li><strong>S3</strong>：对象存储</li>\r\n<li><strong>EBS</strong>：块存储</li>\r\n<li><strong>EFS</strong>：文件系统</li>\r\n</ul>\r\n<h3>数据库服务</h3>\r\n<ul>\r\n<li><strong>RDS</strong>：关系型数据库</li>\r\n<li><strong>DynamoDB</strong>：NoSQL数据库</li>\r\n<li><strong>ElastiCache</strong>：内存缓存</li>\r\n</ul>\r\n<h2>架构设计原则</h2>\r\n<ol>\r\n<li><strong>可靠性</strong>：多可用区部署</li>\r\n<li><strong>安全性</strong>：IAM权限管理</li>\r\n<li><strong>性能效率</strong>：自动扩缩容</li>\r\n<li><strong>成本优化</strong>：按需付费</li>\r\n<li><strong>操作卓越</strong>：监控和日志</li>\r\n</ol>\r\n<h2>实践案例：Web应用架构</h2>\r\n<pre><code># 使用Terraform定义基础设施\r\nresource \"aws_instance\" \"web_server\" {\r\n  ami           = \"ami-0c55b159cbfafe1d0\"\r\n  instance_type = \"t3.micro\"\r\n  \r\n  tags = {\r\n    Name = \"WebServer\"\r\n  }\r\n}\r\n\r\nresource \"aws_s3_bucket\" \"app_bucket\" {\r\n  bucket = \"my-app-storage\"\r\n}</code></pre>\r\n<h2>监控和日志</h2>\r\n<ul>\r\n<li><strong>CloudWatch</strong>：监控指标</li>\r\n<li><strong>X-Ray</strong>：应用追踪</li>\r\n<li><strong>CloudTrail</strong>：API调用日志</li>\r\n</ul>\r\n<h2>成本优化策略</h2>\r\n<ul>\r\n<li>使用预留实例</li>\r\n<li>Spot实例处理批处理任务</li>\r\n<li>自动扩缩容策略</li>\r\n<li>定期审查资源使用情况</li>\r\n</ul>', 'http://192.168.124.23:5000/Upload/2025/09/09/eca3d0a0faf140e691dedcfb0920f00e.jpg', 2, 'published', 1, 234, 0, 0, '2025-09-05 21:00:00', '2025-09-05 20:30:00', '2025-09-19 16:51:38', 'Admin');
INSERT INTO `blog_articles` VALUES (13, 'Python数据分析与可视化实战', '使用pandas、numpy、matplotlib进行数据分析的完整教程', '<h1>Python数据分析与可视化实战</h1>\r\n<p>Python在数据科学领域有着广泛的应用，本文将介绍使用Python进行数据分析和可视化的核心技术。</p>\r\n<h2>核心库介绍</h2>\r\n<h3>NumPy - 数值计算基础</h3>\r\n<pre><code>import numpy as np\r\n\r\n# 创建数组\r\narr = np.array([1, 2, 3, 4, 5])\r\nmatrix = np.array([[1, 2], [3, 4]])\r\n\r\n# 数学运算\r\nresult = np.sqrt(arr)\r\nmean_value = np.mean(arr)</code></pre>\r\n<h3>Pandas - 数据处理利器</h3>\r\n<pre><code>import pandas as pd\r\n\r\n# 读取数据\r\ndf = pd.read_csv(\"data.csv\")\r\n\r\n# 数据清洗\r\ndf_clean = df.dropna()  # 删除空值\r\ndf_clean = df_clean.drop_duplicates()  # 删除重复\r\n\r\n# 数据分组和聚合\r\ngrouped = df.groupby(\"category\").agg({\r\n    \"price\": [\"mean\", \"max\", \"min\"],\r\n    \"quantity\": \"sum\"\r\n})</code></pre>\r\n<h2>数据可视化</h2>\r\n<h3>Matplotlib基础绘图</h3>\r\n<pre><code>import matplotlib.pyplot as plt\r\n\r\n# 折线图\r\nplt.plot(x, y, label=\"数据趋势\")\r\nplt.xlabel(\"时间\")\r\nplt.ylabel(\"数值\")\r\nplt.legend()\r\nplt.show()\r\n\r\n# 柱状图\r\nplt.bar(categories, values)\r\nplt.title(\"销量统计\")</code></pre>\r\n<h3>Seaborn高级可视化</h3>\r\n<pre><code>import seaborn as sns\r\n\r\n# 热力图\r\nsns.heatmap(correlation_matrix, annot=True)\r\n\r\n# 箱线图\r\nsns.boxplot(x=\"category\", y=\"price\", data=df)</code></pre>\r\n<h2>实战案例：销售数据分析</h2>\r\n<ol>\r\n<li><strong>数据收集</strong>：从CSV文件读取销售数据</li>\r\n<li><strong>数据清洗</strong>：处理空值和异常值</li>\r\n<li><strong>探索性分析</strong>：统计描述和相关性分析</li>\r\n<li><strong>可视化展示</strong>：绘制趋势图和分布图</li>\r\n<li><strong>结论总结</strong>：提取业务洞察</li>\r\n</ol>\r\n<h2>进阶技巧</h2>\r\n<ul>\r\n<li>时间序列分析</li>\r\n<li>数据透视表</li>\r\n<li>机器学习集成</li>\r\n<li>交互式可视化</li>\r\n</ul>', 'http://192.168.124.23:5000/Upload/2025/09/09/eca3d0a0faf140e691dedcfb0920f00e.jpg', 1, 'published', 0, 198, 0, 0, '2025-09-05 22:00:00', '2025-09-05 21:45:00', '2025-09-19 16:51:38', 'Admin');
INSERT INTO `blog_articles` VALUES (14, '区块链技术原理与应用开发', '从零开始理解区块链技术，并开发简单的智能合约', '<h1>区块链技术原理与应用开发</h1>\r\n<p>区块链技术作为分布式账本技术，正在改变金融、供应链、数字身份等多个领域。</p>\r\n<h2>区块链基础概念</h2>\r\n<h3>核心特性</h3>\r\n<ul>\r\n<li><strong>去中心化</strong>：没有中央控制节点</li>\r\n<li><strong>不可篡改</strong>：通过密码学保证数据安全</li>\r\n<li><strong>透明性</strong>：所有交易公开可查</li>\r\n<li><strong>共识机制</strong>：网络节点达成一致</li>\r\n</ul>\r\n<h2>区块结构</h2>\r\n<pre><code>class Block {\r\n    constructor(data, previousHash) {\r\n        this.timestamp = Date.now();\r\n        this.data = data;\r\n        this.previousHash = previousHash;\r\n        this.hash = this.calculateHash();\r\n        this.nonce = 0;\r\n    }\r\n    \r\n    calculateHash() {\r\n        return SHA256(\r\n            this.previousHash + \r\n            this.timestamp + \r\n            JSON.stringify(this.data) + \r\n            this.nonce\r\n        ).toString();\r\n    }\r\n}</code></pre>\r\n<h2>智能合约开发</h2>\r\n<h3>Solidity基础</h3>\r\n<pre><code>pragma solidity ^0.8.0;\r\n\r\ncontract SimpleContract {\r\n    string public message;\r\n    address public owner;\r\n    \r\n    constructor(string memory _message) {\r\n        message = _message;\r\n        owner = msg.sender;\r\n    }\r\n    \r\n    function updateMessage(string memory _newMessage) public {\r\n        require(msg.sender == owner, \"Only owner can update\");\r\n        message = _newMessage;\r\n    }\r\n}</code></pre>\r\n<h2>以太坊开发环境</h2>\r\n<ol>\r\n<li><strong>Truffle Suite</strong>：开发框架</li>\r\n<li><strong>Ganache</strong>：本地区块链</li>\r\n<li><strong>MetaMask</strong>：钱包插件</li>\r\n<li><strong>Remix IDE</strong>：在线编辑器</li>\r\n</ol>\r\n<h2>DApp开发实战</h2>\r\n<h3>前端集成Web3.js</h3>\r\n<pre><code>const Web3 = require(\"web3\");\r\nconst web3 = new Web3(\"http://localhost:8545\");\r\n\r\n// 连接智能合约\r\nconst contract = new web3.eth.Contract(abi, contractAddress);\r\n\r\n// 调用合约方法\r\nconst result = await contract.methods.getMessage().call();</code></pre>\r\n<h2>应用场景</h2>\r\n<ul>\r\n<li><strong>数字货币</strong>：比特币、以太坊</li>\r\n<li><strong>供应链追溯</strong>：商品溯源</li>\r\n<li><strong>数字身份</strong>：身份验证</li>\r\n<li><strong>去中心化金融(DeFi)</strong>：借贷、交易</li>\r\n</ul>', 'http://192.168.124.23:5000/Upload/2025/09/09/eca3d0a0faf140e691dedcfb0920f00e.jpg', 1, 'draft', 0, 0, 0, 0, NULL, '2025-09-05 22:15:00', '2025-09-19 16:10:09', 'Admin');
INSERT INTO `blog_articles` VALUES (15, '前端性能优化全面指南', '从加载速度到运行效率，全方位优化前端应用性能', '<h1>前端性能优化全面指南</h1>\r\n<p>前端性能直接影响用户体验，本文将从多个维度介绍前端性能优化的方法和技巧。</p>\r\n<h2>性能指标</h2>\r\n<h3>Core Web Vitals</h3>\r\n<ul>\r\n<li><strong>LCP (Largest Contentful Paint)</strong>：最大内容绘制</li>\r\n<li><strong>FID (First Input Delay)</strong>：首次输入延迟</li>\r\n<li><strong>CLS (Cumulative Layout Shift)</strong>：累积布局偏移</li>\r\n</ul>\r\n<h3>其他重要指标</h3>\r\n<ul>\r\n<li>FCP (First Contentful Paint)</li>\r\n<li>TTI (Time to Interactive)</li>\r\n<li>TTFB (Time to First Byte)</li>\r\n</ul>\r\n<h2>加载性能优化</h2>\r\n<h3>资源压缩</h3>\r\n<pre><code>// Webpack压缩配置\r\nconst TerserPlugin = require(\"terser-webpack-plugin\");\r\n\r\nmodule.exports = {\r\n  optimization: {\r\n    minimize: true,\r\n    minimizer: [new TerserPlugin()],\r\n  },\r\n  plugins: [\r\n    new CompressionPlugin({\r\n      algorithm: \"gzip\",\r\n      test: /.(js|css|html|svg)$/,\r\n    }),\r\n  ],\r\n};</code></pre>\r\n<h3>代码分割</h3>\r\n<pre><code>// React代码分割\r\nconst LazyComponent = React.lazy(() => import(\"./LazyComponent\"));\r\n\r\nfunction App() {\r\n  return (\r\n    &lt;Suspense fallback={&lt;div&gt;Loading...&lt;/div&gt;}&gt;\r\n      &lt;LazyComponent /&gt;\r\n    &lt;/Suspense&gt;\r\n  );\r\n}</code></pre>\r\n<h2>图片优化</h2>\r\n<ul>\r\n<li><strong>格式选择</strong>：WebP、AVIF格式</li>\r\n<li><strong>懒加载</strong>：Intersection Observer</li>\r\n<li><strong>响应式图片</strong>：srcset属性</li>\r\n<li><strong>CDN加速</strong>：全球分发网络</li>\r\n</ul>\r\n<h2>JavaScript优化</h2>\r\n<h3>避免长任务</h3>\r\n<pre><code>// 使用requestIdleCallback分解任务\r\nfunction doWork(deadline) {\r\n  while (deadline.timeRemaining() > 0 && tasks.length > 0) {\r\n    const task = tasks.shift();\r\n    processTask(task);\r\n  }\r\n  \r\n  if (tasks.length > 0) {\r\n    requestIdleCallback(doWork);\r\n  }\r\n}\r\n\r\nrequestIdleCallback(doWork);</code></pre>\r\n<h2>CSS优化</h2>\r\n<ul>\r\n<li>Critical CSS内联</li>\r\n<li>删除未使用的CSS</li>\r\n<li>CSS动画优化</li>\r\n<li>避免强制同步布局</li>\r\n</ul>\r\n<h2>监控和分析</h2>\r\n<ul>\r\n<li><strong>Lighthouse</strong>：性能审计</li>\r\n<li><strong>WebPageTest</strong>：详细分析</li>\r\n<li><strong>Chrome DevTools</strong>：实时调试</li>\r\n<li><strong>Real User Monitoring</strong>：真实用户数据</li>\r\n</ul>', 'http://192.168.124.23:5000/Upload/2025/09/09/eca3d0a0faf140e691dedcfb0920f00e.jpg', 1, 'published', 1, 334, 0, 1, '2025-09-05 23:00:00', '2025-09-05 22:30:00', '2025-09-19 16:51:38', 'Admin');

-- ----------------------------
-- Table structure for blog_comments
-- ----------------------------
DROP TABLE IF EXISTS `blog_comments`;
CREATE TABLE `blog_comments`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `content` text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT '评论内容',
  `target_type` enum('article','project','tool','technology') CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT '评论目标类型：文章/项目/工具/技术栈',
  `target_id` bigint(20) NOT NULL,
  `parent_id` bigint(20) NULL DEFAULT NULL,
  `like_count` int(11) NULL DEFAULT 0 COMMENT '评论点赞数',
  `status` enum('approved','pending','rejected') CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT 'approved' COMMENT '审核状态',
  `ip_address` varchar(45) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT '评论者IP地址',
  `user_agent` text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT '浏览器信息',
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '评论时间',
  `updated_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  `user_id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '评论用户ID，关联Base_User表',
  `depth` tinyint(4) NULL DEFAULT 0 COMMENT '评论层级深度，0为顶级评论',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_comments_target`(`target_type`, `target_id`) USING BTREE,
  INDEX `idx_user_comments`(`user_id`) USING BTREE,
  INDEX `idx_comment_time`(`created_at`) USING BTREE,
  INDEX `idx_status_time`(`status`, `created_at`) USING BTREE,
  INDEX `idx_parent_comments`(`parent_id`) USING BTREE,
  INDEX `idx_target_comments`(`target_type`, `target_id`, `status`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 30 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci COMMENT = '博客评论表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of blog_comments
-- ----------------------------
INSERT INTO `blog_comments` VALUES (1, '这篇文章对Vue响应式系统的解释很清晰，特别是源码分析部分！', 'article', 1, NULL, 1, 'approved', '192.168.1.100', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36', '2025-09-05 12:00:00', '2025-09-22 10:53:13', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (2, '感谢分享，对我理解Vue的实现原理很有帮助', 'article', 1, NULL, 0, 'approved', '192.168.1.101', 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36', '2025-09-05 12:30:00', '2025-09-19 16:51:38', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (6, '方法冯绍峰', 'article', 15, NULL, 0, 'approved', '::ffff:192.168.124.23', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0', '2025-09-09 17:35:44', '2025-09-19 16:10:12', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (4, '博客系统的UI设计很棒，源码什么时候开源？', 'project', 1, NULL, 0, 'approved', '192.168.1.103', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36', '2025-09-05 13:30:00', '2025-09-19 16:51:38', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (5, 'VS Code确实是最好用的编辑器之一，插件生态丰富', 'tool', 1, NULL, 0, 'approved', '192.168.1.104', 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36', '2025-09-05 14:00:00', '2025-09-19 16:51:38', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (7, '你好粉色发', 'article', 10, NULL, 0, 'approved', '::ffff:192.168.124.23', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0', '2025-09-09 17:38:49', '2025-09-19 16:51:38', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (8, '方法', 'article', 10, 7, 0, 'approved', '::ffff:192.168.124.23', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0', '2025-09-09 17:39:06', '2025-09-19 16:51:38', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (9, '方法', 'article', 10, 7, 0, 'approved', '::ffff:192.168.124.23', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0', '2025-09-09 17:39:29', '2025-09-19 16:51:38', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (13, '粉色发发发发额发', 'article', 2, NULL, 2, 'approved', '::ffff:192.168.124.23', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0', '2025-09-16 16:33:48', '2025-09-19 18:15:34', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (11, '阿发我发我发我', 'article', 10, 7, 0, 'approved', '::ffff:192.168.124.23', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0', '2025-09-09 17:40:02', '2025-09-19 16:51:38', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (12, '发撒发额非法恶妇', 'article', 10, NULL, 0, 'approved', '::ffff:192.168.124.23', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0', '2025-09-09 17:43:30', '2025-09-19 16:10:12', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (14, '发色个撒个', 'article', 2, NULL, 1, 'approved', '::ffff:192.168.124.23', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0', '2025-09-16 16:33:54', '2025-09-19 17:58:20', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (15, '哥阿莎傻瓜', 'article', 2, NULL, 1, 'approved', '::ffff:192.168.124.23', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0', '2025-09-16 16:33:59', '2025-09-19 18:16:58', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (16, '噶额尬歌撒嘎嘎饿死鬼', 'article', 2, NULL, 1, 'approved', '::ffff:192.168.124.23', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0', '2025-09-16 16:34:07', '2025-09-19 18:10:00', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (17, '发色个噶发色', 'article', 2, NULL, 1, 'approved', '::ffff:192.168.124.23', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0', '2025-09-16 16:34:12', '2025-09-19 18:05:43', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (18, '噶额噶啥啥啊公司', 'article', 2, NULL, 3, 'approved', '::ffff:192.168.124.23', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0', '2025-09-16 16:34:21', '2025-09-19 18:05:41', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (19, '是的', 'project', 1, 2, 0, 'approved', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0', '2025-09-17 14:30:45', '2025-09-19 16:51:38', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (20, '感谢分享', 'project', 1, 2, 0, 'approved', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0', '2025-09-17 14:40:04', '2025-09-19 16:10:12', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (21, '你好', 'article', 2, 15, 1, 'approved', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0', '2025-09-17 14:49:34', '2025-09-19 17:58:52', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (22, '发发发撒发', 'article', 6, NULL, 0, 'approved', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0', '2025-09-17 16:06:52', '2025-09-19 16:10:12', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (23, '发疯发生发色发顺丰阿是', 'article', 6, NULL, 0, 'approved', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0', '2025-09-17 16:06:57', '2025-09-19 16:10:12', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (24, 'fsfsfefs', 'article', 2, NULL, 2, 'approved', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0', '2025-09-17 16:46:44', '2025-09-19 18:09:01', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (25, '挺好的这篇文章', 'article', 1, NULL, 1, 'approved', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0', '2025-09-19 10:45:54', '2025-09-22 10:53:09', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (26, '感谢分享啊', 'article', 1, NULL, 1, 'approved', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0', '2025-09-19 10:46:22', '2025-09-22 10:54:34', 'Admin', 0);
INSERT INTO `blog_comments` VALUES (27, 'fsfef', 'article', 2, 13, 1, 'approved', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0', '2025-09-19 17:25:05', '2025-09-19 18:04:25', '1', 0);
INSERT INTO `blog_comments` VALUES (28, 'fesfeasfaf', 'article', 2, 13, 1, 'approved', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0', '2025-09-19 17:26:26', '2025-09-19 18:04:28', '1', 0);
INSERT INTO `blog_comments` VALUES (29, '粉色粉色', 'article', 5, 26, 0, 'approved', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0', '2025-09-22 10:54:38', '2025-09-22 10:54:38', '1', 0);

-- ----------------------------
-- Table structure for blog_friendly_links
-- ----------------------------
DROP TABLE IF EXISTS `blog_friendly_links`;
CREATE TABLE `blog_friendly_links`  (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '链接ID',
  `name` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT '链接名称',
  `url` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT '链接地址',
  `description` text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT '链接描述',
  `logo_url` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Logo图片URL',
  `status` tinyint(4) NULL DEFAULT 1 COMMENT '状态：1启用/0禁用',
  `sort_order` int(11) NULL DEFAULT 0 COMMENT '排序权重',
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 4 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci COMMENT = '博客友情链接表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of blog_friendly_links
-- ----------------------------
INSERT INTO `blog_friendly_links` VALUES (2, 'GitHub', 'https://github.com', '全球最大的代码托管平台', '', 1, 20, '2025-09-05 10:05:00');
INSERT INTO `blog_friendly_links` VALUES (3, 'Vue.js官网', 'https://cn.vuejs.org/', 'Vue.js官方网站', '', 1, 30, '2025-09-05 10:10:00');

-- ----------------------------
-- Table structure for blog_hot_comments
-- ----------------------------
DROP TABLE IF EXISTS `blog_hot_comments`;
CREATE TABLE `blog_hot_comments`  (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `comment_id` int(11) NOT NULL COMMENT '评论ID',
  `target_type` enum('article','project','tool','technology') CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '目标类型',
  `target_id` int(11) NOT NULL COMMENT '目标ID',
  `score` int(11) NULL DEFAULT 0 COMMENT '热度分数',
  `updated_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE INDEX `unique_hot_comment`(`comment_id` ASC) USING BTREE,
  INDEX `idx_target_hot`(`target_type` ASC, `target_id` ASC, `score` ASC) USING BTREE,
  INDEX `idx_score_time`(`score` ASC, `updated_at` ASC) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '热门评论缓存表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of blog_hot_comments
-- ----------------------------

-- ----------------------------
-- Table structure for blog_like_stats
-- ----------------------------
DROP TABLE IF EXISTS `blog_like_stats`;
CREATE TABLE `blog_like_stats`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `target_type` enum('article','project','tool','technology','comment') CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `target_id` int(11) NOT NULL,
  `date` date NOT NULL COMMENT '统计日期',
  `like_count` int(11) NULL DEFAULT 0 COMMENT '当日新增点赞数',
  `total_likes` int(11) NULL DEFAULT 0 COMMENT '累计点赞数',
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE INDEX `unique_daily_stats`(`target_type` ASC, `target_id` ASC, `date` ASC) USING BTREE,
  INDEX `idx_date_type`(`date` ASC, `target_type` ASC) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '点赞历史统计表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of blog_like_stats
-- ----------------------------

-- ----------------------------
-- Table structure for blog_likes
-- ----------------------------
DROP TABLE IF EXISTS `blog_likes`;
CREATE TABLE `blog_likes`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '点赞ID，主键',
  `user_id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '点赞用户ID，关联Base_User表',
  `target_type` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '点赞目标类型',
  `target_id` bigint(20) NOT NULL COMMENT '点赞目标ID',
  `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '点赞时间',
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE INDEX `unique_like`(`user_id` ASC, `target_type` ASC, `target_id` ASC) USING BTREE COMMENT '防止重复点赞',
  INDEX `idx_target_likes`(`target_type` ASC, `target_id` ASC) USING BTREE,
  INDEX `idx_user_likes`(`user_id` ASC) USING BTREE,
  INDEX `idx_like_time`(`created_at` ASC) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 63 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '用户点赞表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of blog_likes
-- ----------------------------
INSERT INTO `blog_likes` VALUES (7, '1968968067677425664', 'comment', 14, '2025-09-19 17:58:20');
INSERT INTO `blog_likes` VALUES (11, '1968968067677425664', 'comment', 21, '2025-09-19 17:58:52');
INSERT INTO `blog_likes` VALUES (28, '1968968067677425664', 'article', 7, '2025-09-19 18:02:36');
INSERT INTO `blog_likes` VALUES (29, '1968968067677425664', 'article', 2, '2025-09-19 18:04:16');
INSERT INTO `blog_likes` VALUES (33, '1968968067677425664', 'comment', 27, '2025-09-19 18:04:25');
INSERT INTO `blog_likes` VALUES (34, '1968968067677425664', 'comment', 28, '2025-09-19 18:04:28');
INSERT INTO `blog_likes` VALUES (39, '1968968067677425664', 'comment', 18, '2025-09-19 18:05:41');
INSERT INTO `blog_likes` VALUES (40, '1968968067677425664', 'comment', 17, '2025-09-19 18:05:43');
INSERT INTO `blog_likes` VALUES (46, '1968968067677425664', 'comment', 16, '2025-09-19 18:10:00');
INSERT INTO `blog_likes` VALUES (59, '1968968067677425664', 'comment', 15, '2025-09-19 18:16:58');
INSERT INTO `blog_likes` VALUES (60, 'Admin', 'comment', 25, '2025-09-22 10:53:09');
INSERT INTO `blog_likes` VALUES (61, 'Admin', 'comment', 1, '2025-09-22 10:53:13');
INSERT INTO `blog_likes` VALUES (62, 'Admin', 'comment', 26, '2025-09-22 10:54:34');

-- ----------------------------
-- Table structure for blog_likes_backup
-- ----------------------------
DROP TABLE IF EXISTS `blog_likes_backup`;
CREATE TABLE `blog_likes_backup`  (
  `id` int(11) NOT NULL DEFAULT 0 COMMENT '点赞ID',
  `target_type` enum('article','project','tool','technology','comment') CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT '点赞目标类型：文章/项目/工具/技术栈/评论',
  `target_id` int(11) NOT NULL COMMENT '点赞目标ID',
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '点赞时间',
  `user_id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '点赞用户ID，关联Base_User表',
  `CreatedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '点赞时间'
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of blog_likes_backup
-- ----------------------------
INSERT INTO `blog_likes_backup` VALUES (1, 'article', 1, '2025-09-05 12:05:00', 'Admin', '2025-09-19 17:21:15');
INSERT INTO `blog_likes_backup` VALUES (3, 'article', 2, '2025-09-05 13:05:00', 'Admin', '2025-09-19 17:21:15');
INSERT INTO `blog_likes_backup` VALUES (4, 'project', 1, '2025-09-05 13:35:00', 'Admin', '2025-09-19 17:21:15');
INSERT INTO `blog_likes_backup` VALUES (5, 'tool', 1, '2025-09-05 14:05:00', 'Admin', '2025-09-19 17:21:15');
INSERT INTO `blog_likes_backup` VALUES (6, 'technology', 1, '2025-09-05 14:30:00', 'Admin', '2025-09-19 17:21:15');
INSERT INTO `blog_likes_backup` VALUES (7, 'technology', 2, '2025-09-05 14:45:00', 'Admin', '2025-09-19 17:21:15');
INSERT INTO `blog_likes_backup` VALUES (8, 'technology', 3, '2025-09-05 15:00:00', 'Admin', '2025-09-19 17:21:15');
INSERT INTO `blog_likes_backup` VALUES (9, 'comment', 24, '2025-09-19 17:24:43', '1968968067677425664', '2025-09-19 17:24:43');
INSERT INTO `blog_likes_backup` VALUES (10, 'comment', 18, '2025-09-19 17:24:45', '1968968067677425664', '2025-09-19 17:24:44');
INSERT INTO `blog_likes_backup` VALUES (11, 'comment', 13, '2025-09-19 17:25:25', '1968968067677425664', '2025-09-19 17:25:25');

-- ----------------------------
-- Table structure for blog_project_technologies
-- ----------------------------
DROP TABLE IF EXISTS `blog_project_technologies`;
CREATE TABLE `blog_project_technologies`  (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '关联ID',
  `project_id` int(11) NOT NULL COMMENT '项目ID',
  `technology_id` int(11) NOT NULL COMMENT '技术栈ID',
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `project_id`(`project_id`) USING BTREE,
  INDEX `technology_id`(`technology_id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 7 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci COMMENT = '博客技术栈和项目链接表' ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of blog_project_technologies
-- ----------------------------
INSERT INTO `blog_project_technologies` VALUES (1, 1, 1, '2025-09-05 10:00:00');
INSERT INTO `blog_project_technologies` VALUES (2, 1, 2, '2025-09-05 10:00:00');
INSERT INTO `blog_project_technologies` VALUES (3, 1, 3, '2025-09-05 10:00:00');
INSERT INTO `blog_project_technologies` VALUES (4, 2, 1, '2025-09-05 10:30:00');
INSERT INTO `blog_project_technologies` VALUES (5, 2, 2, '2025-09-05 10:30:00');
INSERT INTO `blog_project_technologies` VALUES (6, 3, 1, '2025-09-05 11:00:00');

-- ----------------------------
-- Table structure for blog_projects
-- ----------------------------
DROP TABLE IF EXISTS `blog_projects`;
CREATE TABLE `blog_projects`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT '项目名称',
  `description` text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT '项目简介描述',
  `content` longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT '项目详细说明，富文本',
  `cover_image` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT '项目封面图片URL',
  `demo_url` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT '项目演示地址',
  `github_url` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'GitHub仓库地址',
  `website_url` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT '其他相关网站地址',
  `status` enum('active','completed','archived') CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT 'active' COMMENT '项目状态：进行中/已完成/已归档',
  `is_featured` tinyint(4) NULL DEFAULT 0 COMMENT '是否为热门项目：1是/0否',
  `difficulty_level` enum('beginner','intermediate','advanced') CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT '项目难度级别',
  `view_count` int(11) NULL DEFAULT 0 COMMENT '浏览次数',
  `like_count` int(11) NULL DEFAULT 0 COMMENT '点赞数量',
  `comment_count` int(11) NULL DEFAULT 0 COMMENT '评论数量',
  `sort_order` int(11) NULL DEFAULT 0 COMMENT '排序权重',
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_projects_featured`(`is_featured`) USING BTREE,
  INDEX `idx_status_featured`(`status`, `is_featured`) USING BTREE,
  INDEX `idx_difficulty_status`(`difficulty_level`, `status`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 9 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci COMMENT = '博客项目表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of blog_projects
-- ----------------------------
INSERT INTO `blog_projects` VALUES (1, '个人博客系统', '基于Vue.js和Node.js开发的全栈个人博客系统', '<h1>个人博客系统</h1><p>这是一个功能完整的个人博客系统，包含文章管理、用户评论、分类标签等功能。</p><h2>技术栈</h2><ul><li>前端：Vue.js + Element UI</li><li>后端：Node.js + Express</li><li>数据库：MySQL</li></ul><h2>主要功能</h2><ul><li>文章发布和管理</li><li>用户注册登录</li><li>评论系统</li><li>分类和标签</li><li>后台管理<img src=\"http://localhost:5000/Upload/2025/09/05/373f6ff54e3a466db8c3c39735cc30de.jpg\" style=\"max-width:100%;\"></li></ul>', 'http://192.168.124.23:5000/Upload/2025/09/09/37f75eb36a354b4f9bb5bd83d11407df.jpg', 'https://blog-demo.example.com', 'https://github.com/only12e/only12eDBK', 'https://example.com', 'active', 1, 'intermediate', 234, 1, 3, 102, '2025-09-05 10:00:00', '2025-09-19 16:51:38');
INSERT INTO `blog_projects` VALUES (2, '任务管理系统', '团队协作的任务管理和项目跟踪系统', '<h1>任务管理系统</h1><p>专为团队协作设计的任务管理系统，支持项目创建、任务分配、进度跟踪等功能。</p><h2>核心功能</h2><ul><li>项目和任务管理</li><li>团队成员协作</li><li>进度跟踪和报告</li><li>时间管理</li><li>文件共享</li></ul><h2>技术特点</h2><ul><li>响应式设计</li><li>实时数据同步</li><li>权限管理</li><li>数据可视化</li></ul>', 'http://192.168.124.23:5000/Upload/2025/09/10/be5dee1c15244c7fbd22f85033dfd473.jpg', 'https://tasks-demo.example.com', 'https://github.com/username/task-manager', NULL, 'active', 1, 'advanced', 178, 0, 0, 20, '2025-09-05 10:30:00', '2025-09-19 16:51:38');
INSERT INTO `blog_projects` VALUES (3, '在线代码编辑器', '支持多种编程语言的在线代码编辑和运行环境', '<h1>在线代码编辑器</h1><p>一个功能丰富的在线代码编辑器，支持语法高亮、代码提示、在线运行等功能。</p><h2>支持语言</h2><ul><li>JavaScript</li><li>Python</li><li>Java</li><li>C++</li><li>HTML/CSS</li></ul><h2>主要特性</h2><ul><li>语法高亮</li><li>代码自动补全</li><li>实时预览</li><li>代码分享</li><li>主题切换</li></ul>', 'http://192.168.124.23:5000/Upload/2025/09/10/ab8fc8c00b1d42b0a3c2a682a81fef07.jpg', 'https://editor-demo.example.com', 'https://github.com/username/code-editor', NULL, 'active', 0, 'intermediate', 145, 0, 0, 30, '2025-09-05 11:00:00', '2025-09-19 16:51:38');

-- ----------------------------
-- Table structure for blog_system_configs
-- ----------------------------
DROP TABLE IF EXISTS `blog_system_configs`;
CREATE TABLE `blog_system_configs`  (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '配置ID',
  `config_key` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT '配置键名',
  `config_value` text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT '配置值',
  `description` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT '配置说明',
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE INDEX `config_key`(`config_key`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 7 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci COMMENT = '博客配置表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of blog_system_configs
-- ----------------------------
INSERT INTO `blog_system_configs` VALUES (1, 'site_title', '我的个人博客', '网站标题', '2025-09-05 10:00:00', '2025-09-09 10:25:15');
INSERT INTO `blog_system_configs` VALUES (2, 'site_description', '代码改变世界，成就个人价值\n分享技术心得，记录编程之路\n', '网站描述', '2025-09-05 10:00:00', '2025-09-09 10:26:24');
INSERT INTO `blog_system_configs` VALUES (3, 'site_keywords', 'Vue.js,Node.js,JavaScript,Web开发,编程,技术博客', '网站关键词', '2025-09-05 10:00:00', '2025-09-05 10:00:00');
INSERT INTO `blog_system_configs` VALUES (4, 'admin_email', 'zhansfai@163.com', '管理员邮箱', '2025-09-05 10:00:00', '2025-09-09 10:26:49');
INSERT INTO `blog_system_configs` VALUES (5, 'posts_per_page', '10', '每页文章数量', '2025-09-05 10:00:00', '2025-09-09 13:50:38');
INSERT INTO `blog_system_configs` VALUES (6, 'comment_enabled', '1', '是否开启评论功能', '2025-09-05 10:00:00', '2025-09-05 10:00:00');

-- ----------------------------
-- Table structure for blog_technologies
-- ----------------------------
DROP TABLE IF EXISTS `blog_technologies`;
CREATE TABLE `blog_technologies`  (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '技术栈ID，主键',
  `name` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT '技术名称，如React、Vue等',
  `logo_url` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT '技术Logo图片URL',
  `description` text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT '技术简短描述，用于列表展示',
  `content` longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT '技术栈详细介绍，富文本，支持文字+图片',
  `category` enum('frontend','backend','database','devtools','other') CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT '技术分类',
  `proficiency_level` int(11) NULL DEFAULT 0 COMMENT '熟练度评分（0-100）',
  `recommendation_level` int(11) NULL DEFAULT 0 COMMENT '推荐度评分（0-100）',
  `is_featured` tinyint(4) NULL DEFAULT 0 COMMENT '是否在首页展示：1是/0否',
  `sort_order` int(11) NULL DEFAULT 0 COMMENT '排序权重',
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_technologies_featured`(`is_featured`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 5 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci COMMENT = '博客技术栈表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of blog_technologies
-- ----------------------------
INSERT INTO `blog_technologies` VALUES (1, 'Vue.js', 'http://192.168.124.23:5000/Upload/2025/09/09/b68f5e02e14043bd8e76f942cf4a41e7.png', 'Vue.js是一个用于构建用户界面的渐进式JavaScript框架', '<h2>Vue.js简介</h2><p>Vue.js是一个用于构建用户界面的渐进式JavaScript框架。它的核心库只关注视图层，易于学习，便于与其它库或已有项目整合。</p><h3>主要特性</h3><ul><li>响应式数据绑定</li><li>组件系统</li><li>虚拟DOM</li><li>指令系统</li></ul>', 'frontend', 90, 95, 1, 10, '2025-09-05 10:00:00', '2025-09-09 17:37:10');
INSERT INTO `blog_technologies` VALUES (2, 'Node.js', 'http://localhost:5000/Upload/2025/09/09/08c4216efa2a41a48c284c4b7b10bc27.png', 'Node.js是一个基于Chrome V8引擎的JavaScript运行时环境', '<h2>Node.js简介</h2><p>Node.js是一个基于Chrome V8 JavaScript引擎构建的运行时环境，可以让JavaScript运行在服务器端。</p><h3>主要特性</h3><ul><li>事件驱动</li><li>非阻塞I/O</li><li>单线程</li><li>跨平台</li></ul>', 'backend', 85, 90, 1, 20, '2025-09-05 10:05:00', '2025-09-09 09:50:30');
INSERT INTO `blog_technologies` VALUES (3, 'MySQL', 'http://192.168.124.23:5000/Upload/2025/09/09/0c8d7dd9fc294041bed4bff5c789107b.jpg', 'MySQL是最流行的关系型数据库管理系统', '<h2>MySQL简介</h2><p>MySQL是最流行的关系型数据库管理系统之一，在Web应用方面，MySQL是最好的RDBMS应用软件之一。</p><h3>主要特性</h3><ul><li>高性能</li><li>可靠性</li><li>易用性</li><li>开源免费</li></ul>', 'database', 80, 85, 1, 30, '2025-09-05 10:10:00', '2025-09-09 14:06:29');

-- ----------------------------
-- Table structure for blog_tools
-- ----------------------------
DROP TABLE IF EXISTS `blog_tools`;
CREATE TABLE `blog_tools`  (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '工具ID，主键',
  `name` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT '工具名称',
  `description` text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT '工具描述说明',
  `content` longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT '工具详细介绍，富文本',
  `cover_image` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT '工具封面图片',
  `icon_url` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT '工具图标URL',
  `official_url` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT '官方网站地址',
  `download_url` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT '下载地址',
  `category` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT '工具分类，如编辑器、浏览器插件等',
  `price_type` enum('free','paid','freemium') CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT 'free' COMMENT '价格类型：免费/付费/免费增值',
  `rating` decimal(2, 1) NULL DEFAULT 0.0 COMMENT '个人评分（0-5）',
  `is_recommended` tinyint(4) NULL DEFAULT 0 COMMENT '是否推荐：1是/0否',
  `sort_order` int(11) NULL DEFAULT 0 COMMENT '排序权重',
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_tools_recommended`(`is_recommended`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 5 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci COMMENT = '博客工具表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of blog_tools
-- ----------------------------
INSERT INTO `blog_tools` VALUES (1, 'Visual Studio Code', 'Microsoft开发的免费开源代码编辑器', '<h1>Visual Studio Code</h1><p>VS Code是Microsoft开发的一款免费、开源的现代化代码编辑器...</p><h2>主要特性</h2><ul><li>智能代码补全</li><li>内置Git支持</li><li>丰富的插件生态</li><li>调试功能</li><li>集成终端</li></ul><h2>推荐插件</h2><ul><li>Auto Rename Tag</li><li>GitLens</li><li>Prettier</li><li>ESLint</li></ul>', 'http://192.168.124.23:5000/Upload/2025/09/12/3c605b66893a4ce789d164c8049deb9e.jpg', 'http://192.168.124.23:5000/Upload/2025/09/12/2c0cfd61df544c789eb9750ec5089883.jpg', 'https://code.visualstudio.com', 'https://code.visualstudio.com/download', '编辑器', 'free', 4.9, 1, 10, '2025-09-05 10:00:00', '2025-09-12 15:35:54');
INSERT INTO `blog_tools` VALUES (2, 'Postman', 'API开发和测试的协作平台', '<h1>Postman</h1><p>Postman是一个用于API开发的协作平台，简化了构建和使用API的流程...</p><h2>核心功能</h2><ul><li>API请求构建</li><li>自动化测试</li><li>API文档生成</li><li>团队协作</li><li>环境管理</li></ul><h2>使用场景</h2><ul><li>API接口测试</li><li>接口文档维护</li><li>团队API协作</li><li>自动化测试脚本</li></ul>', 'http://192.168.124.23:5000/Upload/2025/09/12/f40bbe0963c34bec83462f2630314379.jpg', 'http://192.168.124.23:5000/Upload/2025/09/09/0f11789124ac4cc28608aa1851fc069d.png', 'https://www.postman.com', 'https://www.postman.com/downloads/', 'API测试', 'freemium', 4.7, 1, 20, '2025-09-05 10:15:00', '2025-09-12 15:56:03');
INSERT INTO `blog_tools` VALUES (3, 'Docker Desktop', '容器化应用开发平台', '<h1>Docker Desktop</h1><p>Docker Desktop是在Mac和Windows上运行容器化应用程序的最简单方法...</p><h2>主要优势</h2><ul><li>环境一致性</li><li>应用隔离</li><li>快速部署</li><li>资源高效利用</li></ul><h2>常用命令</h2><pre><code># 拉取镜像\ndocker pull nginx\n\n# 运行容器\ndocker run -d -p 8080:80 nginx\n\n# 查看容器\ndocker ps\n</code></pre>', 'http://192.168.124.23:5000/Upload/2025/09/09/e38d02b9fe0344368aa1f6c8cb0f2715.jpg', 'http://192.168.124.23:5000/Upload/2025/09/09/b01f4623d785488ab7f4346c104fb4b7.png', 'https://www.docker.com', 'https://www.docker.com/products/docker-desktop', '容器化', 'free', 4.5, 1, 30, '2025-09-05 10:30:00', '2025-09-09 17:36:50');

-- ----------------------------
-- Table structure for blog_users
-- ----------------------------
DROP TABLE IF EXISTS `blog_users`;
CREATE TABLE `blog_users`  (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '用户ID，主键',
  `username` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT '用户名，唯一，用于登录',
  `password` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT '密码，加密存储',
  `email` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT '邮箱地址，唯一',
  `nickname` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT '昵称，显示用',
  `avatar` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT '头像URL地址',
  `role` enum('admin','user') CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT 'user' COMMENT '用户角色：admin管理员/user普通用户',
  `status` tinyint(4) NULL DEFAULT 1 COMMENT '账号状态：1启用/0禁用',
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE INDEX `username`(`username`) USING BTREE,
  UNIQUE INDEX `email`(`email`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 8 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci COMMENT = '博客用户表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of blog_users
-- ----------------------------
INSERT INTO `blog_users` VALUES (1, 'admin', 'ktt19HNtGL0yV+j97FYMMysEZ8cpDcDLcdPi9ogFtxo=', 'zhansfai@163.com', '系统管理员', 'http://192.168.124.23:5000/Upload/2025/09/12/d905591dbcae4025a77a9099f62817cb.jpg', 'admin', 1, '2025-09-05 10:00:00', '2025-09-17 17:24:12');
INSERT INTO `blog_users` VALUES (2, 'john_doe', 'e10adc3949ba59abbe56e057f20f883e', 'ff@example.com', 'John Doe', 'http://192.168.124.23:5000/Upload/2025/09/12/37a85d12305e43c4a954795ef4f9d537.jpg', 'user', 1, '2025-09-05 10:15:00', '2025-09-17 14:08:25');
INSERT INTO `blog_users` VALUES (3, 'jane_smith', 'e10adc3949ba59abbe56e057f20f883e', 'jane@example.com', 'Jane Smith', 'http://192.168.124.23:5000/Upload/2025/09/12/8559696e9948449482f37a89b4264ea1.jpg', 'user', 1, '2025-09-05 10:30:00', '2025-09-17 14:33:18');

-- ----------------------------
-- Table structure for blog_views
-- ----------------------------
DROP TABLE IF EXISTS `blog_views`;
CREATE TABLE `blog_views`  (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '浏览记录ID',
  `user_id` int(11) NULL DEFAULT NULL COMMENT '用户ID，游客为NULL',
  `target_type` enum('article','project','tool','technology') CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT '浏览目标类型：文章/项目/工具/技术栈',
  `target_id` int(11) NOT NULL COMMENT '浏览目标ID',
  `ip_address` varchar(45) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT '访问者IP',
  `user_agent` text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT '浏览器信息',
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '浏览时间',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_target`(`target_type`, `target_id`) USING BTREE,
  INDEX `idx_user_time`(`user_id`, `created_at`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 7 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of blog_views
-- ----------------------------
INSERT INTO `blog_views` VALUES (1, 2, 'article', 1, '192.168.1.100', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36', '2025-09-05 11:45:00');
INSERT INTO `blog_views` VALUES (2, 3, 'article', 1, '192.168.1.101', 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36', '2025-09-05 12:15:00');
INSERT INTO `blog_views` VALUES (3, 2, 'article', 1, '192.168.1.105', 'Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36', '2025-09-05 12:45:00');
INSERT INTO `blog_views` VALUES (4, 2, 'article', 2, '192.168.1.102', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36', '2025-09-05 12:50:00');
INSERT INTO `blog_views` VALUES (5, 3, 'project', 1, '192.168.1.103', 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36', '2025-09-05 13:20:00');
INSERT INTO `blog_views` VALUES (6, 2, 'tool', 1, '192.168.1.104', 'Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36', '2025-09-05 13:50:00');

-- ----------------------------
-- Triggers structure for table blog_comments
-- ----------------------------
DROP TRIGGER IF EXISTS `trg_comment_insert`;
delimiter ;;
CREATE TRIGGER `trg_comment_insert` AFTER INSERT ON `blog_comments` FOR EACH ROW BEGIN
    CASE NEW.target_type
        WHEN 'article' THEN
            UPDATE blog_articles SET comment_count = comment_count + 1 WHERE id = NEW.target_id;
        WHEN 'project' THEN
            UPDATE blog_projects SET comment_count = comment_count + 1 WHERE id = NEW.target_id;
    END CASE;
END
;;
delimiter ;

-- ----------------------------
-- Triggers structure for table blog_comments
-- ----------------------------
DROP TRIGGER IF EXISTS `trg_comment_delete`;
delimiter ;;
CREATE TRIGGER `trg_comment_delete` AFTER DELETE ON `blog_comments` FOR EACH ROW BEGIN
    CASE OLD.target_type
        WHEN 'article' THEN
            UPDATE blog_articles SET comment_count = GREATEST(comment_count - 1, 0) WHERE id = OLD.target_id;
        WHEN 'project' THEN
            UPDATE blog_projects SET comment_count = GREATEST(comment_count - 1, 0) WHERE id = OLD.target_id;
    END CASE;
END
;;
delimiter ;

SET FOREIGN_KEY_CHECKS = 1;
