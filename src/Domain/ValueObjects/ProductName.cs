using Domain.Common;

namespace Domain.ValueObjects
{
    public sealed class ProductName : ValueObject
    {
        public string Value { get; }
        public ProductName(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name is required.", nameof(value));
            if (value.Length > 100) throw new ArgumentException("Name length must be <= 100.", nameof(value));
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
