
namespace Domain.Entities.Audit
{
    public interface IActivatable
    {
        bool IsActive { get; }
        void Activate();
        void Deactivate();
    }
}
