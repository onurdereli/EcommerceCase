using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Shared.Dtos;
using Shared.Extensions;
using Web.Client.Abstract;
using Web.Models.ViewModels;

namespace Web.Client.Concrete
{
    public class OrderClient : IOrderClient
    {
        private readonly HttpClient _httpClient;

        public OrderClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Response<IEnumerable<OrderViewModel>>> GetOrdersByUserId(string userId)
        {
            var response = await _httpClient.GetAsync($"Orders/{userId}");

            return await response.ReadFromJsonAsync<Response<IEnumerable<OrderViewModel>>>();
        }
    }
}
