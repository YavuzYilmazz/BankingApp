using Akbank.Base.Entity;

namespace Akbank.Data.Entity;

public class Payment : BaseEntity
{
    public int ExpenseRequestId { get; set; }
    public decimal PaymentAmount { get; set; }
    public DateTime PaymentDate { get; set; }

    public virtual ExpenseRequest ExpenseRequest { get; set; }
}
