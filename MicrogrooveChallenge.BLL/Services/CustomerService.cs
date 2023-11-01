using MicrogrooveChallenge.BLL.Exceptions;
using MicrogrooveChallenge.BLL.Models;
using MicrogrooveChallenge.Data;

namespace MicrogrooveChallenge.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAvatarService _avatarService;

        public CustomerService(ICustomerRepository customerRepository, IAvatarService avatarService)
        {
            _customerRepository = customerRepository;
            _avatarService = avatarService;
        }

        /// <summary>
        /// Finds a specific customer using the customerId.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// <exception cref="CustomerNotFoundException"></exception>
        public async Task<CustomerModel> GetCustomerByIdAsync(Guid customerId)
        {
            var customer = await _customerRepository.GetCustomerByCustomerIdAsync(customerId);

            if(customer == null)
            {
                throw new CustomerNotFoundException($"No customer found with customerId {customerId}.");
            }

            return new CustomerModel(customer);
        }

        /// <summary>
        /// Returns a list of customers that have the given age.
        /// </summary>
        /// <param name="age"></param>
        /// <returns>A list of customers. An empty list if none are found.</returns>
        public async Task<List<CustomerModel>> GetAllCustomersByAgeAsync(int age)
        {
            var customers = await _customerRepository.GetCustomersByAgeAsync(age);

            return customers.Select(x => new CustomerModel(x)).ToList();
        }

        /// <summary>
        /// Creates a customer and fetches an avatar image for them.
        /// </summary>
        /// <param name="customerModel"></param>
        /// <returns></returns>
        /// <exception cref="CustomerModelValidationException"></exception>
        public async Task<CustomerModel> CreateCustomerAsync(CustomerModel customerModel)
        {
            var validationResult = customerModel.IsValid();
            if (validationResult.IsValid)
            {
                var customerAvatar = await _avatarService.CreateCustomerAvatarAsync(customerModel.FullName);
                var customer = await _customerRepository.CreateCustomerAsync(customerModel.FullName, customerModel.DateOfBirth, customerAvatar);

                return new CustomerModel(customer);
            }
            else
            {
                throw new CustomerModelValidationException(validationResult.GetFormattedErrorMessage());
            }
        }
    }
}
