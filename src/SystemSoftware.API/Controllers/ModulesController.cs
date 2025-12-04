using Microsoft.AspNetCore.Mvc;

namespace SystemSoftware.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModulesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var modules = new[]
            {
                new { id = 1, name = "User Management", enabled = true, description = "Manage user accounts and permissions" },
                new { id = 2, name = "Inventory System", enabled = true, description = "Track and manage inventory" },
                new { id = 3, name = "Reporting", enabled = false, description = "Generate business reports" },
                new { id = 4, name = "Billing", enabled = true, description = "Handle invoicing and payments" }
            };

            var response = new
            {
                data = modules,
                navigation = new
                {
                    back = new
                    {
                        url = "/api/home",
                        text = "← Back to Home"
                    },
                    current = "Modules",
                    actions = new[]
                    {
                        new { url = "/api/modules/active", text = "View Active Modules", method = "GET" },
                        new { url = "/api/modules/disable/{id}", text = "Disable Module", method = "POST" },
                        new { url = "/api/modules/enable/{id}", text = "Enable Module", method = "POST" }
                    }
                }
            };

            return Ok(response);
        }

        [HttpGet("active")]
        public IActionResult GetActiveModules()
        {
            var activeModules = new[]
            {
                new { id = 1, name = "User Management", enabled = true, description = "Manage user accounts and permissions" },
                new { id = 2, name = "Inventory System", enabled = true, description = "Track and manage inventory" },
                new { id = 4, name = "Billing", enabled = true, description = "Handle invoicing and payments" }
            };

            var response = new
            {
                data = activeModules,
                navigation = new
                {
                    back = new
                    {
                        url = "/api/modules",
                        text = "← Back to Modules"
                    },
                    current = "Active Modules"
                }
            };

            return Ok(response);
        }

        [HttpPost("enable/{id}")]
        public IActionResult EnableModule(int id)
        {
            var response = new
            {
                message = $"Module {id} enabled successfully",
                navigation = new
                {
                    back = new
                    {
                        url = "/api/modules",
                        text = "← Back to Modules"
                    },
                    current = "Enable Module"
                }
            };

            return Ok(response);
        }

        [HttpPost("disable/{id}")]
        public IActionResult DisableModule(int id)
        {
            var response = new
            {
                message = $"Module {id} disabled successfully",
                navigation = new
                {
                    back = new
                    {
                        url = "/api/modules",
                        text = "← Back to Modules"
                    },
                    current = "Disable Module"
                }
            };

            return Ok(response);
        }
    }
}
