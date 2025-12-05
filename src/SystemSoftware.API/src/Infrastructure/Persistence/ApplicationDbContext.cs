using Microsoft.EntityFrameworkCore;
using SystemSoftware.Domain.Common;
using SystemSoftware.Domain.Entities;
using SystemSoftware.Application.Common.Interfaces;

namespace SystemSoftware.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasIndex(u => u.Username).IsUnique();
                entity.Property(u => u.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseAuditableEntity && 
                           (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (BaseAuditableEntity)entry.Entity;
                var now = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = now;
                    entity.LastModified = now;
                }
                else
                {
                    entity.LastModified = now;
                    Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
