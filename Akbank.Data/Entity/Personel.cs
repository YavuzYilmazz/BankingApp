using Akbank.Base.Entity;

namespace Akbank.Data.Entity;

public class Personel : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string IBAN { get; set; }

    public virtual ICollection<ExpenseRequest> ExpenseRequests { get; set; }
}
