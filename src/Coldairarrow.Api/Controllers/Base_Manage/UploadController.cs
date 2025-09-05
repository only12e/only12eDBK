using Coldairarrow.Util;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag.Annotations;
using System;
using System.IO;
using System.Linq;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    [Route("/Base_Manage/[controller]/[action]")]
    [OpenApiTag("上传")]
    public class UploadController : BaseController
    {
        readonly IConfiguration _configuration;
        public UploadController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult UploadFileByForm(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return JsonContent(new { 
                    errno = 1,
                    message = "未选择文件或文件为空" 
                }.ToJson());

            // 验证文件类型
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
            {
                return JsonContent(new { 
                    errno = 1,
                    message = "不支持的文件类型" 
                }.ToJson());
            }

            // 验证文件大小 (5MB)
            if (file.Length > 5 * 1024 * 1024)
            {
                return JsonContent(new { 
                    errno = 1,
                    message = "文件大小不能超过5MB" 
                }.ToJson());
            }

            try
            {
                // 生成文件路径
                string dateFolder = DateTime.Now.ToString("yyyy/MM/dd");
                string fileName = $"{Guid.NewGuid():N}{extension}";
                string relativePath = $"/Upload/{dateFolder}/{fileName}";
                
                // 使用基类方法获取物理路径
                string physicalPath = GetAbsolutePath($"~{relativePath}");
                
                // 确保目录存在
                string dir = Path.GetDirectoryName(physicalPath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                // 保存文件
                using (FileStream fs = new FileStream(physicalPath, FileMode.Create))
                {
                    file.CopyTo(fs);
                }

                // 构建完整的URL
                string baseUrl = _configuration["WebRootUrl"]?.TrimEnd('/');
                if (string.IsNullOrEmpty(baseUrl))
                {
                    // 如果配置为空，使用当前请求的基础URL
                    var request = HttpContext.Request;
                    baseUrl = $"{request.Scheme}://{request.Host}";
                }
                
                string fullUrl = $"{baseUrl}{relativePath}";
                
                // 返回成功结果
                var result = new
                {
                    errno = 0,
                    data = new[] { fullUrl },
                    // 兼容原有格式
                    name = file.FileName,
                    status = "done",
                    thumbUrl = fullUrl,
                    url = fullUrl
                };

                return JsonContent(result.ToJson());
            }
            catch (Exception ex)
            {
                return JsonContent(new { 
                    errno = 1,
                    message = $"文件上传失败：{ex.Message}" 
                }.ToJson());
            }
        }
    }
}
