
using Domain.Common;

namespace Domain.ValueObjects
{
    public sealed class ProductId : ValueObject
    {
        public Guid Value { get; }
        private ProductId(Guid value) => Value = value;
        public static ProductId New() => new(Guid.NewGuid());
        public static ProductId From(Guid value)
        {
            if (value == default) throw new ArgumentException("ProductId cannot be empty.", nameof(value));
            return new(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
