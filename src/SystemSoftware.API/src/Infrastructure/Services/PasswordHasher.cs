using System;
using System.Security.Cryptography;
using System.Linq;
using SystemSoftware.Domain.Interfaces;

namespace SystemSoftware.Infrastructure.Services
{
    public class PasswordHasher : Domain.Interfaces.IPasswordHasher
    {
        private const int SaltSize = 16; // 128 bits
        private const int KeySize = 32;  // 256 bits
        private const int Iterations = 100000;

        public (string Hash, string Salt) HashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var key = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA512,
                KeySize);
            
            var keyBase64 = Convert.ToBase64String(key);
            var saltBase64 = Convert.ToBase64String(salt);
            
            return ($"{Iterations}.{saltBase64}.{keyBase64}", saltBase64);
        }

        public bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            try
            {
                var parts = storedHash.Split('.', 3);
                var iterations = Convert.ToInt32(parts[0]);
                var salt = Convert.FromBase64String(parts[1]);
                var key = Convert.FromBase64String(parts[2]);
                
                var keyToCheck = Rfc2898DeriveBytes.Pbkdf2(
                    password,
                    salt,
                    iterations,
                    HashAlgorithmName.SHA512,
                    KeySize);
                
                return keyToCheck.SequenceEqual(key);
            }
            catch
            {
                return false;
            }
        }
    }
}
