
using Domain.Common;

namespace Domain.ValueObjects
{
    public sealed class Discount : ValueObject
    { 
        
        public decimal Rate { get; }

        public Discount(decimal rate)
        {
            if (rate < 0 || rate > 1) throw new BussinessException("Rate must be between 0 and 1.", nameof(rate));
            Rate = rate;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Rate;
        }
    }
}