using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SystemSoftware.Application.Common.Interfaces;
using SystemSoftware.Application.Features.Auth.Models;
using SystemSoftware.Domain.Entities;

namespace SystemSoftware.Application.Features.Auth.Commands
{
    public class LoginCommand : IRequest<AuthResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IJwtService _jwtService;
        private readonly Domain.Interfaces.IPasswordHasher _passwordHasher;

        public LoginCommandHandler(
            IApplicationDbContext context,
            IJwtService jwtService,
            Domain.Interfaces.IPasswordHasher passwordHasher)
        {
            _context = context;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user == null || !user.VerifyPassword(request.Password, _passwordHasher))
            {
                if (user != null)
                {
                    user.RecordFailedLogin();
                    await _context.SaveChangesAsync(cancellationToken);
                }
                
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            if (user.IsAccountLocked())
            {
                throw new UnauthorizedAccessException("Account is temporarily locked. Please try again later.");
            }

            user.RecordSuccessfulLogin();
            await _context.SaveChangesAsync(cancellationToken);

            var token = _jwtService.GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            // Save refresh token to database (implementation omitted for brevity)
            // await SaveRefreshTokenAsync(user.Id, refreshToken);

            return new AuthResponse
            {
                Token = token,
                RefreshToken = refreshToken,
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
