using System.Text.Json.Serialization;
using Akbank.Base.Schema;

namespace Akbank.Schema;

public class UserRequest : BaseRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
}

// UserResponse entity
public class UserResponse : BaseResponse
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
}

