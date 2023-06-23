using System.ComponentModel.DataAnnotations;

namespace BankRateAggregator.Domain.Common
{
    public abstract class BaseAuditableEntity
    {
        public DateTimeOffset Created { get; set; }

        [StringLength(36)]
        public string? CreatedBy { get; set; }

        public DateTimeOffset? LastModified { get; set; }

        [StringLength(36)]
        public string? LastModifiedBy { get; set; }
    }
}
