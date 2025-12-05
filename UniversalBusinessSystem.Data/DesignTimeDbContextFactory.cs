using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;

namespace UniversalBusinessSystem.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<UniversalBusinessSystemDbContext>
    {
        public UniversalBusinessSystemDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UniversalBusinessSystemDbContext>();
            
            // Use the actual database path from AppData
            var appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "UniversalBusinessSystem",
                "database",
                "UniversalBusinessSystem.db");
            
            optionsBuilder.UseSqlite($"Data Source={appDataPath}");

            return new UniversalBusinessSystemDbContext(optionsBuilder.Options);
        }
    }
}
