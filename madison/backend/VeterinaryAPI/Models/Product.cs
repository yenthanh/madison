using System.ComponentModel.DataAnnotations;

namespace VeterinaryAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;
        
        public decimal Price { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public bool IsDeleted { get; set; } = false;
        
        public bool IsDangerousDrug { get; set; } = false;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
    }
} 
