using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ProductsAPI.Models.Domain
{
    public class Products
    {
        //If we want to use ID as 6 digit unique number, then we need to set Identity_Insert OFF by using attribute [DatabaseGenerated(DatabaseGeneratedOption.None)]
        // We can use [Key] attribute but if we have ID suffix, EF takes care of this and put it as a Primary Key.
        public int Id { get; set; }

        // 6 digit unique Product Number
        [Required]
        [StringLength(6)]
        public int ProductNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int StockAvailable { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
