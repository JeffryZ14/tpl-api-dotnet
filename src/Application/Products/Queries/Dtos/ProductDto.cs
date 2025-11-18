
namespace Application.Products.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public string Currency { get; init; } = string.Empty;
    }
}
