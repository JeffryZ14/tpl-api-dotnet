
namespace Domain.Entities.Audit
{
    public abstract class ActivatableAggregateRoot : IActivatable//AuditableAggregateRoot,
    {
        public bool IsActive { get; private set; } = true;
    
        public void Activate()
        {
            if (!IsActive)
                IsActive = true;
        }

        public void Deactivate()
        {
            if (IsActive)
                IsActive = false;
        }
    }
}
