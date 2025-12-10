using SystemSoftware.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace SystemSoftware.Core.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Username).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(u => u.IsActive).HasDefaultValue(true);
                entity.Property(u => u.Role).HasDefaultValue("User");
            });
            
            // Store configuration
            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(s => s.IsActive).HasDefaultValue(true);
                entity.HasIndex(s => s.Name).IsUnique();
            });
            
            // Module configuration
            modelBuilder.Entity<Module>(entity =>
            {
                entity.Property(m => m.Name).IsRequired().HasMaxLength(100);
                entity.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(m => m.IsEnabled).HasDefaultValue(true);
                entity.HasOne(m => m.Store).WithMany(s => s.Modules).HasForeignKey(m => m.StoreId);
            });
            
            // Measurement configuration
            modelBuilder.Entity<Measurement>(entity =>
            {
                entity.Property(m => m.Name).IsRequired().HasMaxLength(100);
                entity.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.HasOne(m => m.Store).WithMany(s => s.Measurements).HasForeignKey(m => m.StoreId);
                entity.HasOne(m => m.Module).WithMany(m => m.Measurements).HasForeignKey(m => m.ModuleId);
            });
        }
    }
}
