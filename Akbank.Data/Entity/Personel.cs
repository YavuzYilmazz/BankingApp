using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Akbank.Base.Entity;

namespace Akbank.Data.Entity;

public class Personel : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string IBAN { get; set; }

    public virtual ICollection<ExpenseRequest> ExpenseRequests { get; set; }
}

public class PersonelConfiguration : IEntityTypeConfiguration<Personel>
{
    public void Configure(EntityTypeBuilder<Personel> builder)
    {
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.IBAN).IsRequired().HasMaxLength(34);

        builder.HasMany(x => x.ExpenseRequests)
            .WithOne(x => x.Personel)
            .HasForeignKey(x => x.PersonelId)
            .IsRequired();

        builder.HasKey(x => x.Id);
    }
}
