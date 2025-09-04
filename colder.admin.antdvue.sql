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

 Date: 02/09/2025 18:27:04
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
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '系统权限表' ROW_FORMAT = Dynamic;

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
INSERT INTO `base_action` VALUES ('1193158630615027712', '2019-11-09 21:29:20', 'Admin', 0, '1193158266167758848', 1, '框架介绍', '/Home/Introduce', NULL, 0, NULL, 0);
INSERT INTO `base_action` VALUES ('1193158780011941888', '2019-11-09 21:29:55', 'Admin', 0, '1193158266167758848', 1, '运营统计', '/Home/Statis', NULL, 0, NULL, 0);
INSERT INTO `base_action` VALUES ('1251792477787000832', '2020-04-19 16:39:19', NULL, 0, '1182652266117599232', 2, '增', NULL, 'Base_User.Add', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('1251792477787000833', '2020-04-19 16:39:19', NULL, 0, '1182652266117599232', 2, '改', NULL, 'Base_User.Edit', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('1251792477787000834', '2020-04-19 16:39:19', NULL, 0, '1182652266117599232', 2, '删', NULL, 'Base_User.Delete', 1, NULL, 0);
INSERT INTO `base_action` VALUES ('1251792721769664512', '2020-04-19 16:40:17', 'Admin', 0, '1178957405992521728', 1, '操作日志', '/Base_Manage/Base_UserLog/List', NULL, 0, NULL, 23);

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
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '应用密钥表' ROW_FORMAT = Dynamic;

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
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '生成测试表' ROW_FORMAT = Dynamic;

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
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '数据库连接表' ROW_FORMAT = Dynamic;

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
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '部门表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of base_department
-- ----------------------------
INSERT INTO `base_department` VALUES ('1181175685528424448', '2019-10-07 19:53:23', NULL, 0, '宁波分公司', NULL);
INSERT INTO `base_department` VALUES ('1181175803631636480', '2019-10-07 19:53:51', NULL, 0, '鄞州事业部', '1181175685528424448');
INSERT INTO `base_department` VALUES ('1181175865409540096', '2019-10-07 19:54:06', NULL, 0, '海曙事业部', '1181175685528424448');

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
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '系统角色表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of base_role
-- ----------------------------
INSERT INTO `base_role` VALUES ('1180486275199668224', '2019-10-05 22:13:55', NULL, 0, '超级管理员');
INSERT INTO `base_role` VALUES ('1180819481383931904', '2019-10-06 20:17:57', NULL, 0, '部门管理员');

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
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '角色权限表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of base_roleaction
-- ----------------------------
INSERT INTO `base_roleaction` VALUES ('1188801858282459136', '2019-10-28 20:57:04', NULL, 0, '1180486275199668224', '1182654049414025216');
INSERT INTO `base_roleaction` VALUES ('1188801858282459137', '2019-10-28 20:57:04', NULL, 0, '1180486275199668224', '1182654208411701248');
INSERT INTO `base_roleaction` VALUES ('1188801858282459138', '2019-10-28 20:57:04', NULL, 0, '1180486275199668224', '1183370665412005888');
INSERT INTO `base_roleaction` VALUES ('1188801984434540544', '2019-10-28 20:57:34', NULL, 0, '1180819481383931904', '1188044797802188800');
INSERT INTO `base_roleaction` VALUES ('1188801984434540545', '2019-10-28 20:57:34', NULL, 0, '1180819481383931904', '1188044797802188801');
INSERT INTO `base_roleaction` VALUES ('1188801984434540546', '2019-10-28 20:57:34', NULL, 0, '1180819481383931904', '1182652433302556672');
INSERT INTO `base_roleaction` VALUES ('1188801984434540547', '2019-10-28 20:57:34', NULL, 0, '1180819481383931904', '1178957405992521728');
INSERT INTO `base_roleaction` VALUES ('1188801984434540548', '2019-10-28 20:57:34', NULL, 0, '1180819481383931904', '1188801109783744512');
INSERT INTO `base_roleaction` VALUES ('1188801984434540549', '2019-10-28 20:57:34', NULL, 0, '1180819481383931904', '1188801109783744513');
INSERT INTO `base_roleaction` VALUES ('1188801984434540550', '2019-10-28 20:57:34', NULL, 0, '1180819481383931904', '1188801109783744514');
INSERT INTO `base_roleaction` VALUES ('1188801984434540551', '2019-10-28 20:57:34', NULL, 0, '1180819481383931904', '1182652266117599232');
INSERT INTO `base_roleaction` VALUES ('1188801984434540552', '2019-10-28 20:57:34', NULL, 0, '1180819481383931904', '1188800845714558976');
INSERT INTO `base_roleaction` VALUES ('1188801984434540553', '2019-10-28 20:57:34', NULL, 0, '1180819481383931904', '1188800845714558977');
INSERT INTO `base_roleaction` VALUES ('1188801984434540554', '2019-10-28 20:57:34', NULL, 0, '1180819481383931904', '1188800845714558978');
INSERT INTO `base_roleaction` VALUES ('1188801984434540555', '2019-10-28 20:57:34', NULL, 0, '1180819481383931904', '1182652367447789568');
INSERT INTO `base_roleaction` VALUES ('1188801984434540556', '2019-10-28 20:57:34', NULL, 0, '1180819481383931904', '1188801057778569216');
INSERT INTO `base_roleaction` VALUES ('1188801984434540557', '2019-10-28 20:57:34', NULL, 0, '1180819481383931904', '1188801057778569217');
INSERT INTO `base_roleaction` VALUES ('1188801984434540558', '2019-10-28 20:57:34', NULL, 0, '1180819481383931904', '1188801057778569218');

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
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '系统用户表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of base_user
-- ----------------------------
INSERT INTO `base_user` VALUES ('1181928860648738816', '2019-10-09 21:46:14', NULL, 0, 'xiaohua', 'e10adc3949ba59abbe56e057f20f883e', '小花', 0, NULL, NULL);
INSERT INTO `base_user` VALUES ('1183363221872971776', '2019-10-13 20:45:52', 'Admin', 0, 'aa', NULL, 'aaa', 0, NULL, NULL);
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
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '系统日志表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of base_userlog
-- ----------------------------

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
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '用户角色表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of base_userrole
-- ----------------------------
INSERT INTO `base_userrole` VALUES ('1181927367719784448', '2019-10-09 21:40:18', NULL, 0, '1181922344629702656', '1180819481383931904');
INSERT INTO `base_userrole` VALUES ('1181927367719784449', '2019-10-09 21:40:18', NULL, 0, '1181922344629702656', '1180486275199668224');
INSERT INTO `base_userrole` VALUES ('1181927783786352640', '2019-10-09 21:41:57', NULL, 0, '1181927783727632384', '1180819481383931904');
INSERT INTO `base_userrole` VALUES ('1251792318642524160', '2020-04-19 16:38:41', NULL, 0, '1181928860648738816', '1180819481383931904');

SET FOREIGN_KEY_CHECKS = 1;
