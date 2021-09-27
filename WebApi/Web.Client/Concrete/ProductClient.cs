using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.Dtos;
using Shared.Extensions;
using Web.Client.Abstract;
using Web.Models.Dtos;
using Web.Models.ViewModels;

namespace Web.Client.Concrete
{
    public class ProductClient : IProductClient
    {
        private readonly HttpClient _httpClient;

        public ProductClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Response<ProductViewModel>> CreateProduct(CreateProductDto createProductDto)
        {
            var response = await _httpClient.PostAsJsonAsync("Products", createProductDto);

            return await response.ReadFromJsonAsync<Response<ProductViewModel>>();
        }
        public async Task<Response<ProductViewModel>> GetProductById(string productId)
        {
            var response = await _httpClient.GetAsync($"Products/GetProductById/{productId}");

            return await response.ReadFromJsonAsync<Response<ProductViewModel>>();
        }
        public async Task<Response<List<ProductViewModel>>> GetProducts()
        {
            var response = await _httpClient.GetAsync("Products");
            
            return await response.ReadFromJsonAsync<Response<List<ProductViewModel>>>();
        }
        public async Task<Response<NoContent>> DeleteProduct(string id)
        {
            var response = await _httpClient.DeleteAsync($"Products/{id}");
            
            return response.IsSuccessStatusCode ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("error",500);

        }
    }
}
