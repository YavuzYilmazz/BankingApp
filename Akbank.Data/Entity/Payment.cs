using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Akbank.Base.Entity;

namespace Akbank.Data.Entity;

public class Payment : BaseEntity
{
    public int ExpenseRequestId { get; set; }
    public decimal PaymentAmount { get; set; }
    public DateTime PaymentDate { get; set; }

    public virtual ExpenseRequest ExpenseRequest { get; set; }
}

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.Property(x => x.PaymentAmount).IsRequired();
        builder.Property(x => x.PaymentDate).IsRequired();

        builder.HasOne(x => x.ExpenseRequest)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.ExpenseRequestId)
            .IsRequired();

        builder.HasKey(x => x.Id);
    }
}
