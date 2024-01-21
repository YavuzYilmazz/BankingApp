using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

public class ExpenseRequestConfiguration : IEntityTypeConfiguration<ExpenseRequest>
{
    public void Configure(EntityTypeBuilder<ExpenseRequest> builder)
    {
        builder.Property(x => x.RequestAmount).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Approved).IsRequired();
        builder.Property(x => x.Rejected).IsRequired();

        builder.HasOne(x => x.Personel)
            .WithMany(x => x.ExpenseRequests)
            .HasForeignKey(x => x.PersonelId)
            .IsRequired();

        builder.HasOne(x => x.ExpenseCategory)
            .WithMany(x => x.ExpenseRequests)
            .HasForeignKey(x => x.ExpenseCategoryId)
            .IsRequired();

        builder.HasMany(x => x.Payments)
            .WithOne(x => x.ExpenseRequest)
            .HasForeignKey(x => x.ExpenseRequestId)
            .IsRequired();

        builder.HasKey(x => x.Id);
    }
}
