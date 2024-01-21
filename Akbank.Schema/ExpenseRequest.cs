using System.Text.Json.Serialization;
using Akbank.Base.Schema;
using Akbank.Schema;

namespace Akbank.Data.Entity;


public class ExpenseRequestRequest : BaseRequest
{
    public int PersonelId { get; set; }
    public int ExpenseCategoryId { get; set; }
    public decimal RequestAmount { get; set; }
    public string Description { get; set; }
    public bool Approved { get; set; }
    public bool Rejected { get; set; }
}

// ExpenseRequestResponse entity
public class ExpenseRequestResponse : BaseResponse
{
    public int PersonelId { get; set; }
    public int ExpenseCategoryId { get; set; }
    public decimal RequestAmount { get; set; }
    public string Description { get; set; }
    public bool Approved { get; set; }
    public bool Rejected { get; set; }

    public virtual PersonelResponse Personel { get; set; }
    public virtual ExpenseCategoryResponse ExpenseCategory { get; set; }
    public virtual ICollection<PaymentResponse> Payments { get; set; }
}
