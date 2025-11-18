
namespace Domain.Entities.Audit
{
    public interface IAuditable
    {
        DateTime CreatedOn { get; }
        string CreatedBy { get; }
        DateTime? ModifiedOn { get; }
        string? ModifiedBy { get; }
        void SetCreated(string userId);
        void SetModified(string userId);
    }

}
