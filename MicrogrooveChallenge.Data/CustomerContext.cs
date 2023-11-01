using MicrogrooveChallenge.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MicrogrooveChallenge.Data
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .ToTable("Customer")
                .HasKey(x=> x.CustomerId);

            base.OnModelCreating(modelBuilder);
        }

    }

}
