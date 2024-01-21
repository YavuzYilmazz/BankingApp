using Microsoft.EntityFrameworkCore;
using Akbank.Data.Entity;

namespace Akbank.Data.DbContext;

    public class AkbankDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AkbankDbContext(DbContextOptions<AkbankDbContext> options): base(options)
        {
    
        }   

        public DbSet<Personel> Personnel { get; set; }
        public DbSet<ExpenseRequest> ExpenseRequests { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonelConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseRequestConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }