using System.ComponentModel.DataAnnotations;

namespace Domain.Enum
{
    public enum TransactionStatus
    {
        Initialized = 1,
        Approved,
        Paid,
    }
}