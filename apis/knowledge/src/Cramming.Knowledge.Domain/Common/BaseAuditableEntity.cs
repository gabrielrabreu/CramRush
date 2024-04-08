﻿namespace Cramming.Knowledge.Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        public DateTime CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime LastModifiedOn { get; set; }

        public string? LastModifiedBy { get; set; }
    }
}
