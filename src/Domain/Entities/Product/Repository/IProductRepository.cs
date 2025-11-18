using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Abstractions
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(ProductId id, CancellationToken cancellationToken = default);
        Task AddAsync(Product product, CancellationToken cancellationToken = default);
    }
}
