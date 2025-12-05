using SystemSoftware.Application.Features.Auth.Models;

namespace SystemSoftware.Application.Common.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(SystemSoftware.Domain.Entities.User user);
        int? ValidateJwtToken(string token);
    }
}
