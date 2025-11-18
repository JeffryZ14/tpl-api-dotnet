using Domain.Common;

namespace Domain.Entities.Audit
{
    public abstract class AuditableAggregateRoot : AggregateRoot, IAuditable
    {
        public DateTime CreatedOn { get; private set; }
        public string CreatedBy { get; private set; } = string.Empty;
        public DateTime? ModifiedOn { get; private set; }
        public string? ModifiedBy { get; private set; }

        public void SetCreated(string userId)
        {
            CreatedBy = userId;
            CreatedOn = DateTime.UtcNow;
        }

        public void SetModified(string userId)
        {
            ModifiedBy = userId;
            ModifiedOn = DateTime.UtcNow;
        }
    }
}
