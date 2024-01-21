using System.Text.Json.Serialization;
using Akbank.Base.Schema;
using Akbank.Data.Entity;

namespace Akbank.Schema;

public class ExpenseCategoryRequest : BaseRequest
{
    public string CategoryName { get; set; }
}

// ExpenseCategoryResponse entity
public class ExpenseCategoryResponse : BaseResponse
{
    public string CategoryName { get; set; }

    [JsonIgnore]
    public virtual ICollection<ExpenseRequestResponse> ExpenseRequests { get; set; }
}