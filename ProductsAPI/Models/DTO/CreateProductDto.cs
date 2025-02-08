using System.ComponentModel.DataAnnotations;

namespace ProductsAPI.Models.DTO
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int StockAvailable { get; set; }
    }
}
