using Microsoft.AspNetCore.Mvc;
using SystemSoftware.Core.Models;

namespace SystemSoftware.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var response = new
            {
                message = "System API is working",
                navigation = new
                {
                    back = new
                    {
                        url = "/api/home",
                        text = "← Back to Home"
                    },
                    endpoints = new[]
                    {
                        new { url = "/api/system/info", text = "System Info", method = "GET" },
                        new { url = "/api/system/status", text = "System Status", method = "GET" },
                        new { url = "/api/modules", text = "Modules", method = "GET" }
                    }
                }
            };
            return Ok(response);
        }

        [HttpGet("info")]
        public IActionResult GetInfo()
        {
            var systemInfo = new SystemInfo
            {
                Id = 1,
                OSName = Environment.OSVersion.ToString(),
                OSVersion = Environment.Version.ToString(),
                LastUpdated = DateTime.Now,
                IsActive = true
            };

            var response = new
            {
                data = systemInfo,
                navigation = new
                {
                    back = new
                    {
                        url = "/api/system",
                        text = "← Back to System"
                    },
                    current = "System Information"
                }
            };

            return Ok(response);
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            var response = new
            {
                status = "Healthy",
                uptime = DateTime.Now,
                memory = GC.GetTotalMemory(false),
                navigation = new
                {
                    back = new
                    {
                        url = "/api/system",
                        text = "← Back to System"
                    },
                    current = "System Status"
                }
            };

            return Ok(response);
        }
    }
}
