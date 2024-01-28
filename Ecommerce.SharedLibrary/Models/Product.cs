using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.SharedLibrary.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Required, Range(0.1, 99999.99)]
        public decimal Price { get; set; }
        [Required, DisplayName("Product Image")]
        public string? Base64Img { get; set; }
        [Required, Range(0, 99999)]
        public int Quantity { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime DateUpLoad { get; set; } = DateTime.UtcNow;
    }
}
