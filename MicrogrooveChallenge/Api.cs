using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using MicrogrooveChallenge.BLL.Services;
using Microsoft.Extensions.Logging;
using MicrogrooveChallenge.BLL.Exceptions;
using MicrogrooveChallenge.BLL.Models;
using System.Text.Json;
using MicrogrooveChallenge.ObjectResults;

namespace MicrogrooveChallenge
{
    public class Api
    {
        private readonly ICustomerService _customerService;

        public Api(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [FunctionName("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "customers/{userId:Guid}")] HttpRequest req,
            ILogger log, Guid userId)
        {
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(userId);
                return new OkObjectResult(customer);
            }
            catch (CustomerNotFoundException ex)
            {
                //This exception type is expected so we want to handle it gracefully.
                //Skip logging because this could be a fairly common scenario.
                return new BadRequestObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                //Something unexpected happened here. 
                log.LogError(ex, ex.Message);
                return new InternalServerErrorObjectResult(ex.Message);
            }

        }

        [FunctionName("GetCustomersByAge")]
        public async Task<IActionResult> GetCustomersByAge(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "customers/{age:int}")] HttpRequest req,
            ILogger log, int age)
        {
            try
            {
                var customers = await _customerService.GetAllCustomersByAgeAsync(age);
                return new OkObjectResult(customers);
            }
            catch (Exception ex)
            {
                //Something unexpected happened here. 
                log.LogError(ex, ex.Message);
                return new InternalServerErrorObjectResult(ex.Message);
            }
        }

        [FunctionName("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "customers")] HttpRequest req,
            ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var model = JsonSerializer.Deserialize<CustomerModel>(requestBody);

                var customerModel = await _customerService.CreateCustomerAsync(model);

                return new CreatedObjectResult(customerModel);
            }
            catch (Exception ex)
            {
                //Something unexpected happened here. 
                log.LogError(ex, ex.Message);
                return new InternalServerErrorObjectResult(ex.Message);
            }

        }
    }
}
