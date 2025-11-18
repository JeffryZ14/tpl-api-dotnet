
namespace Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        Task DispatchDomainEventsAsync(CancellationToken cancellationToken = default);
    }
}
