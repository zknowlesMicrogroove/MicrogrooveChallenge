using MicrogrooveChallenge.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrogrooveChallenge.BLL.Services
{
    public class AvatarService : IAvatarService
    {
        private readonly HttpClient _customerAvatarHttpClient;

        public AvatarService(ICustomerRepository customerRepository, IHttpClientFactory httpClientFactory)
        {
            _customerAvatarHttpClient = httpClientFactory.CreateClient("uiAvatarsClient");
        }
        /// <summary>
        /// Makes an Http request to get an avatar image based on a name.
        /// </summary>
        /// <param name="customerName"></param>
        /// <returns></returns>
        public async Task<byte[]> CreateCustomerAvatarAsync(string customerName)
        {
            var avatarRoute = $"api/?name={customerName.Replace(' ', '+')}&format=svg";
            var response = await _customerAvatarHttpClient.GetAsync(avatarRoute);

            var imageByteArray = await response.Content.ReadAsByteArrayAsync();

            return imageByteArray;
        }
    }
}
