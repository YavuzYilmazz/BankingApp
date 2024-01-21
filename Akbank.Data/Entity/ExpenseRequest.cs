using Akbank.Base.Entity;

namespace Akbank.Data.Entity;

public class ExpenseRequest : BaseEntity
{
    public int PersonelId { get; set; }
    public int ExpenseCategoryId { get; set; }
    public decimal RequestAmount { get; set; }
    public string Description { get; set; }
    public bool Approved { get; set; }
    public bool Rejected { get; set; }

    public virtual Personel Personel { get; set; }
    public virtual ExpenseCategory ExpenseCategory { get; set; }
    public virtual ICollection<Payment> Payments { get; set; }
}
