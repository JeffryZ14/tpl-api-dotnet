namespace API.Models.Products
{
    public class UpdateProductPriceRequest
    {
        public decimal Price { get; set; }
        public string Currency { get; set; } = "USD";
    }
}