using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UniversalBusinessSystem.Core.Entities;
using UniversalBusinessSystem.Data;

namespace UniversalBusinessSystem.Services
{
    public interface IAuthService
    {
        Task<(bool Success, string Message, User? User)> RegisterAsync(string username, string email, string password, Guid organizationId, Guid roleId);
        Task<(bool Success, string Message, User? User)> LoginAsync(string username, string password);
        Task<bool> UserExistsAsync(string username, string email, Guid organizationId);
    }

    public class AuthService : IAuthService
    {
        private readonly UniversalBusinessSystemDbContext _context;
        
        public AuthService(UniversalBusinessSystemDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string Message, User? User)> RegisterAsync(string username, string email, string password, Guid organizationId, Guid roleId)
        {
            try
            {
                if (await UserExistsAsync(username, email, organizationId))
                    return (false, "User already exists", null);

                var hash = CreatePasswordHash(password);

                var user = new User
                {
                    Username = username,
                    Email = email,
                    PasswordHash = hash,
                    PasswordSalt = "", // BCrypt handles salt internally
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true,
                    IsEmailVerified = false,
                    OrganizationId = organizationId,
                    RoleId = roleId,
                    FailedLoginAttempts = 0
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return (true, "Registration successful", user);
            }
            catch (Exception ex)
            {
                return (false, $"Registration failed: {ex.Message}", null);
            }
        }

        public async Task<(bool Success, string Message, User? User)> LoginAsync(string username, string password)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username || u.Email == username);
                
                if (user == null)
                    return (false, "User not found", null);
                
                if (!user.IsActive)
                    return (false, "Account is disabled", null);
                
                if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                    return (false, "Invalid password", null);
                
                user.LastLoginAt = DateTime.UtcNow;
                user.FailedLoginAttempts = 0;
                user.LockedUntil = null;
                await _context.SaveChangesAsync();
                
                return (true, "Login successful", user);
            }
            catch (Exception ex)
            {
                return (false, $"Login failed: {ex.Message}", null);
            }
        }

        public async Task<bool> UserExistsAsync(string username, string email, Guid organizationId)
        {
            return await _context.Users.AnyAsync(u => 
                (u.Username == username || u.Email == email) && u.OrganizationId == organizationId);
        }

        private string CreatePasswordHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
