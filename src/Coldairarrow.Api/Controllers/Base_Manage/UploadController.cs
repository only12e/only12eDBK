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
                return Ok(new { 
                    errno = 1,
                    message = "未选择文件或文件为空" 
                });

            // 验证文件类型
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
            {
                return Ok(new { 
                    errno = 1,
                    message = "不支持的文件类型" 
                });
            }

            // 验证文件大小 (5MB)
            if (file.Length > 5 * 1024 * 1024)
            {
                return Ok(new { 
                    errno = 1,
                    message = "文件大小不能超过5MB" 
                });
            }

            try
            {
                // 生成文件路径
                string dateFolder = DateTime.Now.ToString("yyyy/MM/dd");
                string fileName = $"{Guid.NewGuid():N}{extension}";
                string relativePath = $"/Upload/{dateFolder}/{fileName}";
                
                // 获取物理路径，避免使用可能有问题的基类方法
                var webHostEnvironment = HttpContext.RequestServices.GetService<IWebHostEnvironment>();
                string webRootPath = webHostEnvironment?.WebRootPath;
                
                // 如果WebRootPath为空，使用ContentRootPath + wwwroot
                if (string.IsNullOrEmpty(webRootPath))
                {
                    webRootPath = Path.Combine(webHostEnvironment.ContentRootPath, "wwwroot");
                }
                
                // 构建物理路径
                string cleanPath = relativePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
                string physicalPath = Path.Combine(webRootPath, cleanPath);
                
                // 调试信息
                Console.WriteLine($"relativePath: {relativePath}");
                Console.WriteLine($"webRootPath: {webRootPath}");
                Console.WriteLine($"physicalPath: {physicalPath}");
                
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
                
                // 返回成功结果，直接返回URL字符串
                return Ok(new
                {
                    errno = 0,
                    data = new[] { fullUrl },
                    url = fullUrl,
                    name = file.FileName,
                    status = "done",
                    thumbUrl = fullUrl
                });
            }
            catch (Exception ex)
            {
                return Ok(new { 
                    errno = 1,
                    message = $"文件上传失败：{ex.Message}" 
                });
            }
        }
    }
}
