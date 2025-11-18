using Domain.Abstractions;
using Domain.Common;
using MediatR;

namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IMediator _mediator;
        private IProductRepository? _productRepository;

        public UnitOfWork(
            AppDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public IProductRepository Products =>
            _productRepository ??= new EfProductRepository(_context);

        public async Task<int> CommitAsync(
            CancellationToken cancellationToken = default) =>
            await _context.SaveChangesAsync(cancellationToken);

        public async Task DispatchDomainEventsAsync(
            CancellationToken cancellationToken = default)
        {
            var domainEntities = _context.ChangeTracker
                .Entries<Entity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(e => e.DomainEvents)
                .ToList();

            domainEntities.ForEach(e => e.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }
        }
    }
}
