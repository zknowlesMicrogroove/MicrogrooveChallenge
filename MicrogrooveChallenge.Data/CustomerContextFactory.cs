using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace MicrogrooveChallenge.Data
{
    /// <summary>
    /// Used for running migrations and creating a context for the data seeder console app.
    /// </summary>
    public class CustomerContextFactory : IDesignTimeDbContextFactory<CustomerContext>
    {
        public CustomerContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CustomerContext>();

            optionsBuilder.UseSqlite(DatabaseFileLocator.GetConnectionString());

            return new CustomerContext(optionsBuilder.Options);
        }
    }
}
