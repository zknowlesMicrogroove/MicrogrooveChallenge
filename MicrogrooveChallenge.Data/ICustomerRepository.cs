using MicrogrooveChallenge.Data.Models;

namespace MicrogrooveChallenge.Data
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomerByCustomerIdAsync(Guid customerId);
        Task<List<Customer>> GetCustomersByAgeAsync(int age);
        Task<Customer> CreateCustomerAsync(string fullName, DateOnly dateOfBirth, byte[] avatar);
    }
}