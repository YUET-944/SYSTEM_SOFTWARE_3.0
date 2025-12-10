using SystemSoftware.Core.Models;
using SystemSoftware.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace SystemSoftware.Core.Services
{
    public class DataSeeder
    {
        private readonly AppDbContext _context;

        public DataSeeder(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedModulesForStore(int storeId)
        {
            // Check if modules already exist for this store
            var existingModules = await _context.Modules
                .Where(m => m.StoreId == storeId)
                .ToListAsync();

            if (existingModules.Any())
                return; // Modules already exist for this store

            // Create default modules
            var modules = new List<Module>
            {
                new Module
                {
                    Name = "User Management",
                    Description = "Manage user accounts and permissions",
                    IsEnabled = true,
                    StoreId = storeId,
                    CreatedAt = DateTime.UtcNow
                },
                new Module
                {
                    Name = "Inventory System",
                    Description = "Track and manage inventory",
                    IsEnabled = true,
                    StoreId = storeId,
                    CreatedAt = DateTime.UtcNow
                },
                new Module
                {
                    Name = "Reporting",
                    Description = "Generate business reports",
                    IsEnabled = false,
                    StoreId = storeId,
                    CreatedAt = DateTime.UtcNow
                },
                new Module
                {
                    Name = "Billing",
                    Description = "Handle invoicing and payments",
                    IsEnabled = true,
                    StoreId = storeId,
                    CreatedAt = DateTime.UtcNow
                }
            };

            _context.Modules.AddRange(modules);
            await _context.SaveChangesAsync();

            // Create some sample measurements
            var measurements = new List<Measurement>
            {
                new Measurement
                {
                    Name = "Product Count",
                    Unit = "items",
                    MinValue = 0,
                    MaxValue = 10000,
                    StoreId = storeId,
                    ModuleId = modules[1].Id, // Inventory System
                    CreatedAt = DateTime.UtcNow
                },
                new Measurement
                {
                    Name = "Monthly Revenue",
                    Unit = "USD",
                    MinValue = 0,
                    MaxValue = 1000000,
                    StoreId = storeId,
                    ModuleId = modules[3].Id, // Billing
                    CreatedAt = DateTime.UtcNow
                },
                new Measurement
                {
                    Name = "Active Users",
                    Unit = "users",
                    MinValue = 1,
                    MaxValue = 1000,
                    StoreId = storeId,
                    ModuleId = modules[0].Id, // User Management
                    CreatedAt = DateTime.UtcNow
                }
            };

            _context.Measurements.AddRange(measurements);
            await _context.SaveChangesAsync();
        }
    }
}
