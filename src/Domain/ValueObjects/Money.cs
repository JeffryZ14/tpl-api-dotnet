using Domain.Common;

namespace Domain.ValueObjects
{
    public sealed class Money : ValueObject
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Money(decimal amount, string currency = "USD")
        {
            if (amount < 0) throw new BussinessException("Amount cannot be negative.", nameof(amount));
            if (string.IsNullOrWhiteSpace(currency)) throw new BussinessException("Currency is required.", nameof(currency));
            Amount = amount;
            Currency = currency;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}
