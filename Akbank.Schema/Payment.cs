using System.Text.Json.Serialization;
using Akbank.Base.Schema;
using Akbank.Data.Entity;

namespace Akbank.Schema;

public class PaymentRequest : BaseRequest
{
    public int ExpenseRequestId { get; set; }
    public decimal PaymentAmount { get; set; }
    public DateTime PaymentDate { get; set; }
}

// PaymentResponse entity
public class PaymentResponse : BaseResponse
{
    public int ExpenseRequestId { get; set; }
    public decimal PaymentAmount { get; set; }
    public DateTime PaymentDate { get; set; }

    public virtual ExpenseRequestResponse ExpenseRequest { get; set; }
}
