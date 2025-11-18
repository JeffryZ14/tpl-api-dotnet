using Domain.Common;
using Domain.Entities.Audit;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Product : ActivatableAggregateRoot
    {
        public ProductId Id { get; private set; }
        public ProductName Name { get; private set; }
        public Money Price { get; private set; }

        private Product() { }

        public Product(ProductName name, Money price, string createdBy)
        {
            Id = ProductId.New();
            Name = name;
            Price = price;
            //SetCreated(createdBy);
            //AddDomainEvent(new ProductCreatedEvent(Id.Value));
        }

        public void UpdatePrice(Money newPrice, string modifiedBy)
        {
            if (newPrice.Amount <= 0)
                throw new BussinessException("Price must be greater than zero.", nameof(newPrice));
            Price = newPrice;
            //SetModified(modifiedBy);
            //AddDomainEvent(new ProductPriceChangedEvent(Id.Value, newPrice));
        }


    }
}