using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SystemSoftware.Domain.Entities;

namespace SystemSoftware.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
