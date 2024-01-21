using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Akbank.Base.Entity;

namespace Akbank.Data.Entity;

public class ExpenseCategory : BaseEntity
{
    public string CategoryName { get; set; }

    public virtual ICollection<ExpenseRequest> ExpenseRequests { get; set; }
}

public class ExpenseCategoryConfiguration : IEntityTypeConfiguration<ExpenseCategory>
{
    public void Configure(EntityTypeBuilder<ExpenseCategory> builder)
    {
        builder.Property(x => x.CategoryName).IsRequired();

        builder.HasMany(x => x.ExpenseRequests)
            .WithOne(x => x.ExpenseCategory)
            .HasForeignKey(x => x.ExpenseCategoryId)
            .IsRequired();

        builder.HasKey(x => x.Id);
    }
}
