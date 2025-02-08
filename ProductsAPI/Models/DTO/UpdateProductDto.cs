using System.ComponentModel.DataAnnotations;

namespace ProductsAPI.Models.DTO
{
    public class UpdateProductDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
