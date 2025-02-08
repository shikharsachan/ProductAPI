namespace ProductsAPI.Models.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public int ProductNumber { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int StockAvailable { get; set; }
    }
}
