using Microsoft.AspNetCore.Mvc;

namespace SystemSoftware.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var response = new
            {
                message = "Welcome to Universal Business System API",
                version = "1.0.0",
                navigation = new
                {
                    current = "Home",
                    endpoints = new[]
                    {
                        new { url = "/api/system", text = "System Management", method = "GET" },
                        new { url = "/api/modules", text = "Module Management", method = "GET" },
                        new { url = "/api/weatherforecast", text = "Weather Forecast", method = "GET" }
                    }
                }
            };

            return Ok(response);
        }
    }
}
