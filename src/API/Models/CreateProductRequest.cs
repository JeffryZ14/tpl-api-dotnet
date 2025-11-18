namespace API.Models.Products
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Currency { get; set; } = "USD";
    }
}