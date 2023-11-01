using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MicrogrooveChallenge.ObjectResults
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object value = null)
           : base(value)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}
