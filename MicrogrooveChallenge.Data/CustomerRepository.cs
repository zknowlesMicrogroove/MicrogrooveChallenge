using MicrogrooveChallenge.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MicrogrooveChallenge.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;
        public CustomerRepository(CustomerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Finds a given customer by customerId.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>The customer if found. Null if not customer is found.</returns>
        public async Task<Customer?> GetCustomerByCustomerIdAsync(Guid customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);

            return customer;
        }

        /// <summary>
        /// Gets a list of customers with the given age.
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public async Task<List<Customer>> GetCustomersByAgeAsync(int age)
        {
            var birthYear = DateTime.Now.AddYears(-age);
            var customers = await _context.Customers.Where(x => x.DateOfBirth.Year == birthYear.Year).ToListAsync();

            return customers;
        }

        /// <summary>
        /// Creates a customer in the Database. Does not perform validation.
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="avatar"></param>
        /// <returns>The created customer.</returns>
        public async Task<Customer> CreateCustomerAsync(string fullName, DateOnly dateOfBirth, byte[] avatar)
        {
            //Typically I would have the DB generate this primary key value for me.
            var customerId = Guid.NewGuid();

            var customer = new Customer()
            {
                CustomerId = customerId,
                FullName = fullName,
                DateOfBirth = dateOfBirth,
                Avatar = avatar
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }
    }
}
