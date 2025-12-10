using Microsoft.AspNetCore.Mvc;
using SystemSoftware.Core.Data;
using Microsoft.EntityFrameworkCore;
using SystemSoftware.Core.Models;

namespace SystemSoftware.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModulesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public ModulesController(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Get store ID from middleware
            var context = _httpContextAccessor.HttpContext;
            if (context?.Items["StoreId"] is not int storeId)
            {
                return Unauthorized(new { message = "Store information not found" });
            }

            var modules = await _context.Modules
                .Include(m => m.Measurements)
                .Where(m => m.StoreId == storeId)
                .ToListAsync();

            var response = new
            {
                data = modules.Select(m => new {
                    id = m.Id,
                    name = m.Name,
                    enabled = m.IsEnabled,
                    description = m.Description,
                    storeId = m.StoreId,
                    measurements = m.Measurements.Select(me => new {
                        id = me.Id,
                        name = me.Name,
                        unit = me.Unit,
                        minValue = me.MinValue,
                        maxValue = me.MaxValue
                    }).ToList()
                }),
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
        public async Task<IActionResult> GetActiveModules()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context?.Items["StoreId"] is not int storeId)
            {
                return Unauthorized(new { message = "Store information not found" });
            }

            var activeModules = await _context.Modules
                .Include(m => m.Measurements)
                .Where(m => m.IsEnabled && m.StoreId == storeId)
                .ToListAsync();

            var response = new
            {
                data = activeModules.Select(m => new {
                    id = m.Id,
                    name = m.Name,
                    enabled = m.IsEnabled,
                    description = m.Description,
                    storeId = m.StoreId,
                    measurements = m.Measurements.Select(me => new {
                        id = me.Id,
                        name = me.Name,
                        unit = me.Unit,
                        minValue = me.MinValue,
                        maxValue = me.MaxValue
                    }).ToList()
                }),
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
        public async Task<IActionResult> EnableModule(int id)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context?.Items["StoreId"] is not int storeId)
            {
                return Unauthorized(new { message = "Store information not found" });
            }

            var module = await _context.Modules
                .FirstOrDefaultAsync(m => m.Id == id && m.StoreId == storeId);
                
            if (module == null)
            {
                return NotFound(new { message = "Module not found or not accessible" });
            }

            module.IsEnabled = true;
            module.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

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
        public async Task<IActionResult> DisableModule(int id)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context?.Items["StoreId"] is not int storeId)
            {
                return Unauthorized(new { message = "Store information not found" });
            }

            var module = await _context.Modules
                .FirstOrDefaultAsync(m => m.Id == id && m.StoreId == storeId);
                
            if (module == null)
            {
                return NotFound(new { message = "Module not found or not accessible" });
            }

            module.IsEnabled = false;
            module.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

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
