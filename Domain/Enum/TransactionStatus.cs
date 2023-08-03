using System.ComponentModel.DataAnnotations;

namespace Domain.Enum
{
    public enum TransactionStatus
    {
        [Display(Name = "Awaiting Approval")]
        Pending,
        Approved,
        Rejected,
    }
}