using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SystemSoftware.Application.Common.Interfaces;
using SystemSoftware.Application.Features.Auth.Models;

namespace SystemSoftware.Application.Features.Auth.Commands
{
    public class RefreshTokenCommand : IRequest<AuthResponse>
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly IJwtService _jwtService;
        private readonly IApplicationDbContext _context;

        public RefreshTokenCommandHandler(
            IJwtService jwtService,
            IApplicationDbContext context)
        {
            _jwtService = jwtService;
            _context = context;
        }

        public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var userId = _jwtService.ValidateJwtToken(request.Token);
            
            if (userId == null)
            {
                throw new UnauthorizedAccessException("Invalid token");
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId.Value, cancellationToken);

            if (user == null || !user.IsActive)
            {
                throw new UnauthorizedAccessException("User not found or inactive");
            }

            // In a real implementation, you would validate the refresh token from the database
            // For now, we'll just generate a new token
            var newToken = _jwtService.GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken();

            return new AuthResponse
            {
                Token = newToken,
                RefreshToken = newRefreshToken,
                User = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                }
            };
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
