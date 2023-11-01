using FluentAssertions;
using MicrogrooveChallenge.BLL.Models;

namespace MicrogrooveChallenge.Tests.UnitTests
{

    public class CustomerModelValidationTests
    {
        private CustomerModel _customerModel;
        private ModelValidationResult _customerModelValidationResult;

        [Test]
        public void ShouldPassValidation()
        {
            GivenAValidCustomerModel();
            WhenValidating();
            ThenModelValidationResult().IsValid.Should().BeTrue();
            ThenModelValidationResult().ErrorMessages.Should().BeEmpty();
        }

        [Test]
        public void ShouldFailFullNameValidation()
        {
            GivenACustomerModelWithInvalidName();
            WhenValidating();
            ThenModelValidationResult().IsValid.Should().BeFalse();
            ThenModelValidationResult().ErrorMessages.Should().HaveCount(1);
        }

        [Test]
        public void ShouldFailDateOfBirthValidation()
        {
            GivenACustomerWithAnInvalidDateOfBirth();
            WhenValidating();
            ThenModelValidationResult().IsValid.Should().BeFalse();
            ThenModelValidationResult().ErrorMessages.Should().HaveCount(1);
        }


        [Test]
        public void ShouldFailAllValidation()
        {
            GivenACustomerModelWithABunchOfIssues();
            WhenValidating();
            ThenModelValidationResult().IsValid.Should().BeFalse();
            ThenModelValidationResult().ErrorMessages.Should().HaveCount(2);
        }

        private void GivenACustomerWithAnInvalidDateOfBirth()
        {
            _customerModel = new CustomerModel()
            {
                CustomerId = Guid.NewGuid(),
                FullName = "Invalid CustomerModel",
                DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(2)),
            };
        }

        private void GivenACustomerModelWithABunchOfIssues()
        {
            _customerModel = new CustomerModel()
            {
                CustomerId = Guid.NewGuid(),
                DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(5)),
            };
        }

        private void GivenACustomerModelWithInvalidName()
        {
            _customerModel = new CustomerModel()
            {
                CustomerId = Guid.NewGuid(),
                DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-25))
            };
        }

        private ModelValidationResult ThenModelValidationResult()
        {
            return _customerModelValidationResult;
        }

        private void WhenValidating()
        {
            _customerModelValidationResult = _customerModel.IsValid();
        }

        private void GivenAValidCustomerModel()
        {
            _customerModel = new CustomerModel()
            {
                CustomerId = Guid.NewGuid(),
                FullName = "Valid CustomerModel",
                DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-35)),
            };
        }
    }
}
