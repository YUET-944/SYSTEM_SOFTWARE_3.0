using System;
using System.ComponentModel.DataAnnotations;

namespace UniversalBusinessSystem.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        
        [Required]
        public string PasswordSalt { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsActive { get; set; } = true;
        public string Role { get; set; } = "User";
    }
}
