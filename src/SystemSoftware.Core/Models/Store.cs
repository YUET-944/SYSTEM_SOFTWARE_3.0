using System.ComponentModel.DataAnnotations;

namespace SystemSoftware.Core.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(200)]
        public string? Description { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        
        // Navigation properties
        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<Module> Modules { get; set; } = new List<Module>();
        public virtual ICollection<Measurement> Measurements { get; set; } = new List<Measurement>();
    }
}
