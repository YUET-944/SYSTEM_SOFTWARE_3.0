using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using UniversalBusinessSystem.Core.Entities;
using UniversalBusinessSystem.Data;

namespace UniversalBusinessSystem.Services;

public class DatabaseService
{
    private readonly ILogger<DatabaseService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public DatabaseService(ILogger<DatabaseService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task<bool> InitializeDatabaseAsync()
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<UniversalBusinessSystemDbContext>();
            
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();
            
            // Check if we have seed data
            if (!await context.ShopTypes.AnyAsync())
            {
                await SeedDataAsync(context);
            }
            
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to initialize database");
            Debug.WriteLine($"[DatabaseService] Initialization failed: {ex}");
            return false;
        }
    }

    public async Task<bool> CreateOrganizationAsync(Organization organization, User user, ShopType shopType)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<UniversalBusinessSystemDbContext>();

            // Start transaction
            using var transaction = await context.Database.BeginTransactionAsync();
            
            try
            {
                // Add organization
                context.Organizations.Add(organization);
                await context.SaveChangesAsync();
                
                // Set user organization and role
                user.OrganizationId = organization.Id;
                
                // Get or create default admin role
                var adminRole = await context.Roles
                    .FirstOrDefaultAsync(r => r.Name == "Admin" && r.OrganizationId == organization.Id);
                
                if (adminRole == null)
                {
                    adminRole = new Role
                    {
                        Name = "Admin",
                        Description = "System Administrator",
                        OrganizationId = organization.Id,
                        CreatedAt = DateTime.UtcNow
                    };
                    context.Roles.Add(adminRole);
                    await context.SaveChangesAsync();
                }
                
                user.RoleId = adminRole.Id;
                
                // Add user
                context.Users.Add(user);
                await context.SaveChangesAsync();
                
                // Add organization modules based on shop type
                await AddOrganizationModulesAsync(context, organization.Id, shopType);
                
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create organization");
            Debug.WriteLine($"[DatabaseService] CreateOrganization failed: {ex}");
            return false;
        }
    }

    private async Task AddOrganizationModulesAsync(UniversalBusinessSystemDbContext context, Guid organizationId, ShopType shopType)
    {
        // Get modules from shop type's default modules
        var defaultModuleIds = ParseJsonGuidList(shopType.DefaultModules ?? string.Empty);
        
        foreach (var moduleId in defaultModuleIds)
        {
            var module = await context.Modules.FindAsync(moduleId);
            if (module != null)
            {
                context.OrganizationModules.Add(new OrganizationModule
                {
                    OrganizationId = organizationId,
                    ModuleId = moduleId,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                });
            }
        }
        
        await context.SaveChangesAsync();
    }

    private async Task AddCustomUnitsAsync(UniversalBusinessSystemDbContext context, Guid organizationId, List<Guid> unitIds)
    {
        foreach (var unitId in unitIds)
        {
            var unit = await context.Units.FindAsync(unitId);
            if (unit != null)
            {
                context.OrganizationUnits.Add(new OrganizationUnit
                {
                    OrganizationId = organizationId,
                    UnitId = unitId,
                    CreatedAt = DateTime.UtcNow
                });
            }
        }
        
        await context.SaveChangesAsync();
    }

    private async Task SeedDataAsync(UniversalBusinessSystemDbContext context)
    {
        // Seed data is already handled in DbContext's SeedData method
        // This is just a placeholder
        await Task.CompletedTask;
    }

    private List<Guid> ParseJsonGuidList(string json)
    {
        try
        {
            if (string.IsNullOrEmpty(json))
                return new List<Guid>();
            return System.Text.Json.JsonSerializer.Deserialize<List<Guid>>(json) ?? new List<Guid>();
        }
        catch
        {
            return new List<Guid>();
        }
    }
}
