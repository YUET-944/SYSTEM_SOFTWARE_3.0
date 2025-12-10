using SystemSoftware.Core.Models;
using SystemSoftware.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace SystemSoftware.Core.Services
{
    public interface IAuthService
    {
        Task<(User user, string token)> RegisterAsync(string username, string email, string password, string? storeName = null);
        Task<(User user, string token)> LoginAsync(string username, string password);
        Task<bool> UserExistsAsync(string username, string email);
    }

    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;
        
        public AuthService(AppDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<(User user, string token)> RegisterAsync(string username, string email, string password, string? storeName = null)
        {
            // Check if user exists
            if (await UserExistsAsync(username, email))
                throw new Exception("User already exists");

            // Create or find store
            Store store;
            if (string.IsNullOrEmpty(storeName))
            {
                // Create default store with username
                storeName = $"{username}'s Store";
                store = new Store
                {
                    Name = storeName,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };
                _context.Stores.Add(store);
                await _context.SaveChangesAsync();
            }
            else
            {
                store = await _context.Stores.FirstOrDefaultAsync(s => s.Name == storeName);
                if (store == null)
                {
                    store = new Store
                    {
                        Name = storeName,
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    };
                    _context.Stores.Add(store);
                    await _context.SaveChangesAsync();
                }
            }

            // Create password hash
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                Role = "User",
                StoreId = store.Id
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Generate JWT token
            var token = _jwtService.GenerateToken(user);

            return (user, token);
        }

        public async Task<(User user, string token)> LoginAsync(string username, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username || u.Email == username);

            if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new Exception("Invalid credentials");

            // Update last login
            user.LastLogin = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            // Generate JWT token
            var token = _jwtService.GenerateToken(user);

            return (user, token);
        }

        public async Task<bool> UserExistsAsync(string username, string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Username == username || u.Email == email);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(storedHash);
        }
    }
}
