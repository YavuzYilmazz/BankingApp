using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Akbank.Base.Entity;

namespace Akbank.Data.Entity;

public class User : BaseEntity
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.UserName).IsRequired();
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.IsAdmin).IsRequired();

        builder.HasKey(x => x.Id);
    }
}
