using System;
using System.ComponentModel.DataAnnotations;
using SystemSoftware.Domain.Common;
using SystemSoftware.Domain.Interfaces;

namespace SystemSoftware.Domain.Entities
{
    public class User : BaseAuditableEntity
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string PasswordSalt { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsEmailVerified { get; set; } = false;
        public DateTime? LastLoginAt { get; set; }
        public int FailedLoginAttempts { get; set; } = 0;
        public DateTime? LockedUntil { get; set; }

        // Domain methods
        public static User Create(string username, string email, string password, IPasswordHasher passwordHasher)
        {
            var (hash, salt) = passwordHasher.HashPassword(password);
            
            return new User
            {
                Username = username,
                Email = email,
                PasswordHash = hash,
                PasswordSalt = salt,
                CreatedAt = DateTime.UtcNow
            };
        }

        public bool VerifyPassword(string password, IPasswordHasher passwordHasher)
        {
            return passwordHasher.VerifyPassword(password, PasswordHash, PasswordSalt);
        }

        public void RecordSuccessfulLogin()
        {
            LastLoginAt = DateTime.UtcNow;
            FailedLoginAttempts = 0;
            LockedUntil = null;
        }

        public void RecordFailedLogin(int maxAttempts = 5, int lockoutMinutes = 15)
        {
            FailedLoginAttempts++;
            
            if (FailedLoginAttempts >= maxAttempts)
            {
                LockedUntil = DateTime.UtcNow.AddMinutes(lockoutMinutes);
            }
        }

        public bool IsAccountLocked()
        {
            return LockedUntil.HasValue && LockedUntil > DateTime.UtcNow;
        }
    }
}
