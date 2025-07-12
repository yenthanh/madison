using System.ComponentModel.DataAnnotations;

namespace VeterinaryAPI.Models
{
    public class UpdateProductDescriptionDto
    {
        [Required]
        public int ProductId { get; set; }
        
        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
    }
} 
