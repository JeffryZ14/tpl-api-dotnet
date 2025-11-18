using Domain.Common;

namespace Domain.Entities
{
    public class ProductCreatedEvent : DomainEvent
    {
        public Guid ProductId { get; }
        public ProductCreatedEvent(Guid productId) => ProductId = productId;
    }
}
