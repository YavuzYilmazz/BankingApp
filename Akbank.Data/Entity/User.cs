using Akbank.Base.Entity;

namespace Akbank.Data.Entity;

public class User : BaseEntity
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
}
