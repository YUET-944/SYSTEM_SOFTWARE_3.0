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
    public class RegisterCommand : IRequest<AuthResponse>
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly Domain.Interfaces.IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public RegisterCommandHandler(
            IApplicationDbContext context,
            Domain.Interfaces.IPasswordHasher passwordHasher,
            IJwtService jwtService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Check if user already exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email || u.Username == request.Username, cancellationToken);

            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email or username already exists");
            }

            var user = User.Create(
                request.Username,
                request.Email,
                request.Password,
                _passwordHasher);

            if (!string.IsNullOrEmpty(request.FirstName))
                user.FirstName = request.FirstName;

            if (!string.IsNullOrEmpty(request.LastName))
                user.LastName = request.LastName;

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            var token = _jwtService.GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

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
