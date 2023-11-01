using MicrogrooveChallenge.BLL.JsonConverters;
using MicrogrooveChallenge.Data.Models;
using System.Text.Json.Serialization;

namespace MicrogrooveChallenge.BLL.Models
{
    public class CustomerModel
    {
        public Guid? CustomerId { get; set; }
        public string FullName { get; set; }

        [property: JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly DateOfBirth { get; set; }

        public byte[] Avatar { get; set; }

        public CustomerModel() { }

        //Typically I would use an automapper or something here.
        public CustomerModel(Customer customer)
        {
            CustomerId = customer.CustomerId;
            FullName = customer.FullName;
            DateOfBirth = customer.DateOfBirth;
            Avatar = customer.Avatar;
        }

        /// <summary>
        /// Validates the model.
        /// </summary>
        /// <returns>An object that includes the validity and a list of errors if not valid.</returns>
        public ModelValidationResult IsValid()
        {
            var validationResult = new ModelValidationResult();
            validationResult.IsValid = true;

            if(string.IsNullOrWhiteSpace(FullName))
            {
                validationResult.IsValid = false;
                validationResult.ErrorMessages.Add("FullName is required.");
            }

            if(DateOfBirth.Year > DateTime.Now.Year)
            {
                validationResult.IsValid = false;
                validationResult.ErrorMessages.Add("Date of birth cannot be in a future year.");
            }

            return validationResult;
        }
    }
}
