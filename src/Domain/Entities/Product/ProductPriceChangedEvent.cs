using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class ProductPriceChangedEvent : DomainEvent
    {
        public Guid ProductId { get; }
        public Money NewPrice { get; }
        public ProductPriceChangedEvent(Guid productId, Money newPrice)
        {
            ProductId = productId;
            NewPrice = newPrice;
        }
    }
}
