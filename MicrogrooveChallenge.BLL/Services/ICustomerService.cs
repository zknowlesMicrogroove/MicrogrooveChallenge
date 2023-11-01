using MicrogrooveChallenge.BLL.Models;

namespace MicrogrooveChallenge.BLL.Services
{
    public interface ICustomerService
    {
        Task<CustomerModel> CreateCustomerAsync(CustomerModel customer);
        Task<List<CustomerModel>> GetAllCustomersByAgeAsync(int age);
        Task<CustomerModel> GetCustomerByIdAsync(Guid customerId);
    }
}