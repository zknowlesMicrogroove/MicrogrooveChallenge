using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MicrogrooveChallenge.ObjectResults
{
    public class CreatedObjectResult : ObjectResult
    {
        public CreatedObjectResult(object value = null)
            : base(value)
        {
            StatusCode = (int)HttpStatusCode.Created;
        }
    }
}
