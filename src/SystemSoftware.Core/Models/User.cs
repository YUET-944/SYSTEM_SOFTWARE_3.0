using System;
using System.ComponentModel.DataAnnotations;

namespace SystemSoftware.Core.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        
        [Required]
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsActive { get; set; }
        public string? Role { get; set; }
        
        // Foreign key for Store
        [Required]
        public int StoreId { get; set; }
        
        // Navigation properties
        public virtual Store Store { get; set; } = null!;
    }
}
