using FluentAssertions;
using MicrogrooveChallenge.BLL.Exceptions;
using MicrogrooveChallenge.BLL.Models;
using MicrogrooveChallenge.BLL.Services;
using MicrogrooveChallenge.Data;
using MicrogrooveChallenge.Data.Models;
using Moq;

namespace MicrogrooveChallenge.Tests
{
    public class CustomerServiceTests
    {
        private CustomerModel _customerModel;
        private CustomerModel _createdCustomerModel;
        private CustomerService _customerService;

        [SetUp]
        public void Setup()
        {
            var mockAvatarService = new Mock<IAvatarService>();
            mockAvatarService.Setup(x => x.CreateCustomerAvatarAsync(It.IsAny<string>())).ReturnsAsync(new byte[10]);

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(x => x.CreateCustomerAsync(It.IsAny<string>(), It.IsAny<DateOnly>(), It.IsAny<byte[]>())).ReturnsAsync(new Customer()
            {
                CustomerId = Guid.NewGuid(),
                FullName = "Created Customer",
                DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
                Avatar = new byte[10]
            });

            _customerService = new CustomerService(mockCustomerRepository.Object, mockAvatarService.Object);
        }

        [Test]
        public void ShouldCreateACustomer()
        {
            GivenAValidCustomerModel();
            WhenCreatingACustomer();
            ThenCustomerShouldBeCreated();
        }

        [Test]
        public void ShouldThrowDueToCustomerModelValidationError()
        {
            GivenAnInvalidCustomerModel();
            WhenCreatingACustomerThenAnExceptionIsThrown();
        }

        private void ThenCustomerShouldBeCreated()
        {
            _createdCustomerModel.Should().NotBeNull();
        }

        private void WhenCreatingACustomer()
        {
            _createdCustomerModel = _customerService.CreateCustomerAsync(_customerModel).Result;
        }

        private void GivenAValidCustomerModel()
        {
            _customerModel = new CustomerModel()
            {
                FullName = "Valid Customer",
                DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-35))
            };
        }

        private void WhenCreatingACustomerThenAnExceptionIsThrown()
        {
            _customerService.Invoking(x => x.CreateCustomerAsync(_customerModel))
            .Should().ThrowAsync<CustomerModelValidationException>();
        }

        private void GivenAnInvalidCustomerModel()
        {
            _customerModel = new CustomerModel()
            {
                DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-35))
            };
        }
    }
}