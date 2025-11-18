using Domain.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Infrastructure.Persistence
{
    public class EfProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public EfProductRepository(AppDbContext context) => _context = context;

        public async Task<Product?> GetByIdAsync(ProductId id, CancellationToken cancellationToken = default)
        {
            return await _context.Products.FindAsync(id, cancellationToken);
        }

        public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(product, cancellationToken);
        }
    }
}
