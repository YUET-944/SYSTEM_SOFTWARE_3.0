using System.ComponentModel.DataAnnotations;

namespace SystemSoftware.Core.Models
{
    public class Measurement
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string? Unit { get; set; }
        
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public DateTime CreatedAt { get; set; }
        
        // Foreign keys
        [Required]
        public int StoreId { get; set; }
        public int? ModuleId { get; set; }
        
        // Navigation properties
        public virtual Store Store { get; set; } = null!;
        public virtual Module? Module { get; set; }
    }
}
