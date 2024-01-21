using System.Text.Json.Serialization;
using Akbank.Base.Schema;
using Akbank.Data.Entity;

namespace Akbank.Schema;

public class PersonelRequest : BaseRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string IBAN { get; set; }
}

// PersonelResponse entity
public class PersonelResponse : BaseResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string IBAN { get; set; }

    [JsonIgnore]
    public virtual ICollection<ExpenseRequestResponse> ExpenseRequests { get; set; }
}