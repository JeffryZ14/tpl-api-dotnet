
using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Factories
{
    public interface IProductFactory
    {
        Product Create(ProductName name, Money price, string createBy);
    }
    public class ProductFactory : IProductFactory
    {
        public Product Create(ProductName name, Money price, string createBy)
        {
            return new Product(name, price, createBy);
        }
    }

}