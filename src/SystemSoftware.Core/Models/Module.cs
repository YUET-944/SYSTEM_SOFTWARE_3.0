using System.ComponentModel.DataAnnotations;

namespace SystemSoftware.Core.Models
{
    public class Module
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        public bool IsEnabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Foreign key for Store
        [Required]
        public int StoreId { get; set; }
        
        // Navigation properties
        public virtual Store Store { get; set; } = null!;
        public virtual ICollection<Measurement> Measurements { get; set; } = new List<Measurement>();
    }
}
