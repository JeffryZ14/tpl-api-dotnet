using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Services
{

    public interface IPiceCalculator
    {
        Money CalculateFinalPrice(Product product, Discount discount);

    }
    public class PiceCalculator : IPiceCalculator
    {
        public Money CalculateFinalPrice(Product product, Discount discount)
        {
            var discounted = product.Price.Amount *(1- discount.Rate);
            return new Money(discounted, product.Price.Currency);
        }
    }
}