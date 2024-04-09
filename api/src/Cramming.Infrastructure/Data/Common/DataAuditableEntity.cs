namespace Cramming.Infrastructure.Data.Common
{
    public abstract class DataAuditableEntity : DataEntity
    {
        public DateTime CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime LastModifiedOn { get; set; }

        public string? LastModifiedBy { get; set; }
    }
}
