# OnlyBlog 博客系统 - 生产环境部署指南

## 📋 项目概述

OnlyBlog 是一个现代化的个人博客系统，基于 .NET 5 和 Vue.js 构建，提供完整的博客管理和展示功能。系统采用前后端分离架构，支持技术文章、项目展示、工具推荐等多种内容类型。

### 基础框架

本项目基于 **Colder.Admin.AntdVue** 框架开发，这是一个优秀的 Web 后台快速开发框架，采用 .NET5 + Ant Design Vue 技术栈。

**框架资源：**
- **代码(GitHub)：** <https://github.com/Coldairarrow/Colder.Admin.AntdVue>  
- **文档(GitHub)：** <https://github.com/Coldairarrow/Colder.Admin.AntdVue/wiki>  
- **代码(码云镜像)：** <https://gitee.com/Coldairarrow/Colder.Admin.AntdVue>  
- **文档(码云镜像)：** <https://gitee.com/Coldairarrow/Colder.Admin.AntdVue/wikis>  
- **在线预览地址：** <http://coldairarrow.gitee.io/colder.amin.antdvue.preview.web/> （账号/密码：Admin 123456）

### 项目特色

在 Colder.Admin.AntdVue 基础框架之上，OnlyBlog 扩展实现了：
- 🎨 现代化的博客展示前台页面
- 📝 完整的技术文章管理系统
- 🚀 项目展示和介绍功能
- 🛠️ 开发工具推荐系统
- ✨ 玻璃形态设计 + 粒子动效
- 📱 全面的响应式适配

## 项目页面展示

### 管理端：

![75747919581](G:\Project\only12eDBK\1757479195817.png)

![75747924739](G:\Project\only12eDBK\1757479247396.png)

### 用户端：

![75747933019](G:\Project\only12eDBK\1757479330195.png)

![75747935573](C:\Users\htl616\AppData\Local\Temp\1757479355733.png)

### 页面设计特色

**🎨 现代化UI设计**
- **玻璃形态设计 (Glassmorphism)**: 所有卡片和组件采用半透明玻璃效果，配合模糊背景和渐变边框，营造现代科技感
- **渐变色彩方案**: 使用 `linear-gradient(135deg, #667eea 0%, #764ba2 100%)` 主色调，统一的紫蓝色系
- **圆角设计**: 16px 圆角半径，柔和的视觉效果
- **阴影层次**: 多层次阴影效果 `0 8px 32px rgba(102, 126, 234, 0.1)`

**✨ 动态粒子特效系统**
- **浮动粒子动画**: 页面背景包含 20-50 个动态粒子，随机分布并缓慢浮动
- **鼠标交互响应**: 粒子会根据鼠标位置进行偏移，营造沉浸式交互体验
- **自适应数量**: 不同页面根据复杂度调整粒子数量（首页50个，详情页20个）
- **性能优化**: 使用 CSS Transform 和 GPU 加速，确保流畅的动画效果

**🌟 交互动效**
- **悬停效果**: 卡片悬停时上移 4px 并增强阴影 `transform: translateY(-4px)`
- **平滑过渡**: 所有交互采用 `cubic-bezier(0.4, 0, 0.2, 1)` 缓动函数
- **微弹效果**: 按钮点击时轻微缩放和颜色变化
- **滚动视差**: 背景粒子随滚动产生轻微视差效果

**📱 响应式设计**
- **移动优先**: 采用移动优先的响应式设计策略
- **断点适配**: 768px、1200px 主要断点，完美适配手机、平板、桌面
- **布局重排**: 大屏网格布局，小屏堆叠布局
- **字体缩放**: 根据设备尺寸自动调整字体大小

**🎭 视觉层次**
- **信息架构**: 清晰的信息层次和视觉重量分配
- **色彩对比**: 充分的颜色对比度，确保可访问性
- **留白艺术**: 合理的留白空间，避免视觉拥挤
- **品牌一致性**: 统一的设计语言贯穿整个系统



## 🏗️ 系统架构

### 技术栈

**后端 (API)**
- **框架**: .NET 5.0 Web API
- **ORM**: Entity Framework Core 5.0 + EFCore.Sharding
- **认证**: JWT Bearer Token
- **数据库**: MySQL 8.0+ (主要) / SQL Server / PostgreSQL / Oracle (可选)
- **缓存**: Redis / Memory Cache
- **日志**: Serilog + Elasticsearch (可选)
- **文档**: NSwag (Swagger)

**前端 (Web)**
- **框架**: Vue.js 2.5+ + Vue Router + Vuex
- **UI库**: Ant Design Vue 1.5.3
- **构建工具**: Vue CLI 3.7+
- **图表**: ECharts 6.0
- **富文本**: WangEditor 3.1.1
- **样式**: Less + CSS3

### 项目结构

```
📦 OnlyBlog
├── 📁 src/                          # 源码目录
│   ├── 📁 Coldairarrow.Api/          # Web API 项目
│   ├── 📁 Coldairarrow.Business/     # 业务逻辑层
│   ├── 📁 Coldairarrow.IBusiness/    # 业务接口层
│   ├── 📁 Coldairarrow.Entity/       # 实体层
│   ├── 📁 Coldairarrow.Util/         # 工具类库
│   └── 📁 Coldairarrow.Web/          # Vue 前端项目
├── 📄 Colder.Admin.AntdVue.sln      # 解决方案文件
├── 📄 colder.admin.antdvue.sql      # 数据库脚本
├── 📄 博客系统数据库设计.md          # 数据库设计文档
└── 📄 README.md                     # 项目说明
```

## 🚀 生产环境部署

### 系统要求

**服务器环境**
- **操作系统**: Linux (Ubuntu 20.04+/CentOS 8+) 或 Windows Server 2019+
- **运行时**: .NET 5.0 Runtime
- **内存**: 最低 2GB，推荐 4GB+
- **存储**: 最低 20GB，推荐 50GB+
- **网络**: 公网IP或域名

**依赖服务**
- **数据库**: MySQL 8.0+ (推荐) 或其他支持的数据库
- **缓存**: Redis 6.0+ (可选，推荐用于生产环境)
- **Web服务器**: Nginx 1.18+ (用于反向代理和静态文件服务)

### 1. 数据库准备

#### 1.1 MySQL 数据库安装
```bash
# Ubuntu/Debian
sudo apt update
sudo apt install mysql-server-8.0

# CentOS/RHEL
sudo yum install mysql-server
```

#### 1.2 创建数据库和用户
```sql
-- 创建数据库
CREATE DATABASE Colder_Admin_AntdVue CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- 创建用户
CREATE USER 'blog_user'@'%' IDENTIFIED BY 'your_strong_password';

-- 授权
GRANT ALL PRIVILEGES ON Colder_Admin_AntdVue.* TO 'blog_user'@'%';
FLUSH PRIVILEGES;
```

#### 1.3 导入数据库结构
```bash
mysql -u blog_user -p Colder_Admin_AntdVue < colder.admin.antdvue.sql
```

### 2. Redis 安装配置 (可选但推荐)

```bash
# Ubuntu/Debian
sudo apt install redis-server

# CentOS/RHEL
sudo yum install redis

# 启动服务
sudo systemctl start redis
sudo systemctl enable redis

# 设置密码 (编辑 /etc/redis/redis.conf)
requirepass your_redis_password
```

### 3. 后端 API 部署

#### 3.1 编译发布
```bash
# 在开发环境执行
cd src/Coldairarrow.Api
dotnet publish -c Release -o ./publish --self-contained false
```

#### 3.2 配置文件修改
编辑 `appsettings.json`:
```json
{
  "urls": "http://*:5000",
  "AllowedHosts": "*",
  "Database": {
    "BaseDb": {
      "DatabaseType": "MySql",
      "ConnectionString": "server=localhost;user id=blog_user;password=your_db_password;persistsecurityinfo=True;database=Colder_Admin_AntdVue;SslMode=none"
    }
  },
  "Cache": {
    "CacheType": "Redis",
    "RedisEndpoint": "127.0.0.1:6379,password=your_redis_password"
  },
  "WebRootUrl": "https://yourdomain.com",
  "jwt": {
    "secret": "YOUR-256-BIT-SECRET-KEY-HERE",
    "accessExpireHours": 24,
    "refreshExpireHours": 168
  }
}
```

#### 3.3 部署到服务器
```bash
# 上传发布文件到服务器
scp -r ./publish user@server:/opt/onlyblog-api/

# 在服务器上安装 .NET 运行时
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt update
sudo apt install aspnetcore-runtime-5.0
```

#### 3.4 创建系统服务
创建 `/etc/systemd/system/onlyblog-api.service`:
```ini
[Unit]
Description=OnlyBlog API
After=network.target

[Service]
Type=notify
ExecStart=/usr/bin/dotnet /opt/onlyblog-api/Coldairarrow.Api.dll
Restart=always
RestartSec=10
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```

```bash
# 启动服务
sudo systemctl daemon-reload
sudo systemctl start onlyblog-api
sudo systemctl enable onlyblog-api
```

### 4. 前端部署

#### 4.1 编译构建
```bash
cd src/Coldairarrow.Web

# 安装依赖
npm install

# 生产环境构建
npm run build
```

#### 4.2 部署静态文件
```bash
# 上传构建产物到服务器
scp -r ./dist user@server:/var/www/onlyblog/
```

#### 4.3 Nginx 配置
创建 `/etc/nginx/sites-available/onlyblog`:
```nginx
server {
    listen 80;
    server_name yourdomain.com www.yourdomain.com;
    
    # 重定向到 HTTPS
    return 301 https://$server_name$request_uri;
}

server {
    listen 443 ssl http2;
    server_name yourdomain.com www.yourdomain.com;
    
    # SSL 配置
    ssl_certificate /path/to/your/certificate.crt;
    ssl_certificate_key /path/to/your/private.key;
    ssl_protocols TLSv1.2 TLSv1.3;
    ssl_ciphers HIGH:!aNULL:!MD5;
    
    # 前端静态文件
    location / {
        root /var/www/onlyblog;
        index index.html;
        try_files $uri $uri/ /index.html;
        
        # 静态资源缓存
        location ~* \.(js|css|png|jpg|jpeg|gif|ico|svg|woff|woff2|ttf|eot)$ {
            expires 1y;
            add_header Cache-Control "public, immutable";
        }
    }
    
    # API 代理
    location /api/ {
        proxy_pass http://127.0.0.1:5000;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
        
        # 处理跨域
        add_header Access-Control-Allow-Origin *;
        add_header Access-Control-Allow-Methods 'GET, POST, OPTIONS';
        add_header Access-Control-Allow-Headers 'DNT,X-Mx-ReqToken,Keep-Alive,User-Agent,X-Requested-With,If-Modified-Since,Cache-Control,Content-Type,Authorization';
    }
    
    # Swagger 文档
    location /swagger/ {
        proxy_pass http://127.0.0.1:5000;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
    
    # 安全配置
    add_header X-Frame-Options DENY;
    add_header X-Content-Type-Options nosniff;
    add_header X-XSS-Protection "1; mode=block";
    add_header Strict-Transport-Security "max-age=63072000; includeSubDomains; preload";
}
```

```bash
# 启用站点
sudo ln -s /etc/nginx/sites-available/onlyblog /etc/nginx/sites-enabled/
sudo nginx -t
sudo systemctl reload nginx
```

### 5. SSL 证书配置

#### 使用 Let's Encrypt 免费证书
```bash
# 安装 Certbot
sudo apt install certbot python3-certbot-nginx

# 获取证书
sudo certbot --nginx -d yourdomain.com -d www.yourdomain.com

# 自动续期
sudo crontab -e
# 添加: 0 12 * * * /usr/bin/certbot renew --quiet
```

### 6. Docker 部署 (可选)

#### 6.1 构建后端镜像
```dockerfile
# Dockerfile (已包含在项目中)
FROM registry.cn-hangzhou.aliyuncs.com/colder-public/aspnet:5.0.2
RUN rm -f /etc/localtime && ln -sv /usr/share/zoneinfo/Asia/Shanghai /etc/localtime
WORKDIR /app
COPY . .
EXPOSE 5000
ENTRYPOINT ["dotnet","Coldairarrow.Api.dll"]
```

#### 6.2 Docker Compose 配置
```yaml
version: '3.8'

services:
  mysql:
    image: mysql:8.0
    container_name: onlyblog-mysql
    environment:
      MYSQL_ROOT_PASSWORD: root_password
      MYSQL_DATABASE: Colder_Admin_AntdVue
      MYSQL_USER: blog_user
      MYSQL_PASSWORD: blog_password
    volumes:
      - mysql_data:/var/lib/mysql
      - ./colder.admin.antdvue.sql:/docker-entrypoint-initdb.d/init.sql
    ports:
      - "3306:3306"
    restart: unless-stopped

  redis:
    image: redis:6.2-alpine
    container_name: onlyblog-redis
    command: redis-server --requirepass redis_password
    ports:
      - "6379:6379"
    restart: unless-stopped

  api:
    build: ./src/Coldairarrow.Api
    container_name: onlyblog-api
    ports:
      - "5000:5000"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
    depends_on:
      - mysql
      - redis
    restart: unless-stopped

  nginx:
    image: nginx:alpine
    container_name: onlyblog-nginx
    ports:
      - "80:80"
      - "443:443"
    volumes:
      - ./nginx.conf:/etc/nginx/nginx.conf
      - ./dist:/var/www/onlyblog
      - ./ssl:/etc/nginx/ssl
    depends_on:
      - api
    restart: unless-stopped

volumes:
  mysql_data:
```

## 🔧 运维监控

### 1. 日志管理
```bash
# 查看 API 日志
sudo journalctl -u onlyblog-api -f

# 查看 Nginx 日志
sudo tail -f /var/log/nginx/access.log
sudo tail -f /var/log/nginx/error.log
```

### 2. 性能监控
- 使用 htop 监控系统资源
- 配置 MySQL 慢查询日志
- 使用 Redis 监控工具监控缓存性能

### 3. 备份策略
```bash
# 数据库备份脚本
#!/bin/bash
DATE=$(date +%Y%m%d_%H%M%S)
mysqldump -u blog_user -p blog_password Colder_Admin_AntdVue > /backup/onlyblog_$DATE.sql
find /backup -name "onlyblog_*.sql" -mtime +7 -delete
```

### 4. 更新部署
```bash
# 后端更新
sudo systemctl stop onlyblog-api
# 替换文件
sudo systemctl start onlyblog-api

# 前端更新
# 替换 /var/www/onlyblog 目录
sudo systemctl reload nginx
```

## 🔐 安全配置

### 1. 防火墙设置
```bash
# UFW 配置
sudo ufw allow ssh
sudo ufw allow 80
sudo ufw allow 443
sudo ufw enable
```

### 2. 数据库安全
- 禁用 root 远程登录
- 定期更新密码
- 开启二进制日志

### 3. 应用安全
- 定期更新 JWT Secret
- 配置 HTTPS 强制跳转
- 设置请求频率限制

## 🚨 故障排除

### 常见问题

1. **API 无法启动**
   - 检查数据库连接
   - 验证 .NET 运行时版本
   - 查看日志文件

2. **前端无法访问 API**
   - 检查 Nginx 代理配置
   - 验证防火墙设置
   - 检查 CORS 配置

3. **数据库连接失败**
   - 验证连接字符串
   - 检查数据库服务状态
   - 确认用户权限

### 性能优化

1. **数据库优化**
   - 添加必要的索引
   - 优化查询语句
   - 配置连接池

2. **缓存优化**
   - 启用 Redis 缓存
   - 配置静态文件缓存
   - 使用 CDN 加速

3. **前端优化**
   - 启用 Gzip 压缩
   - 配置浏览器缓存
   - 使用图片压缩

## 📞 技术支持

如遇到部署问题，请查看：
- 项目 GitHub Issues
- 数据库设计文档: `博客系统数据库设计.md`
- API 文档: https://yourdomain.com/swagger

---

## 📝 更新日志

### v1.0.0 (2025-01-XX)
- ✅ 完整的博客系统功能
- ✅ 技术文章管理和展示
- ✅ 项目展示功能
- ✅ 工具推荐系统
- ✅ 响应式设计
- ✅ 管理后台

---

**注意**: 请确保在生产环境中修改所有默认密码和密钥，并定期进行安全更新。