using Akbank.Base.Entity;

namespace Akbank.Data.Entity;

public class ExpenseCategory : BaseEntity
{
    public string CategoryName { get; set; }

    public virtual ICollection<ExpenseRequest> ExpenseRequests { get; set; }
}

