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
    public class CampaignClient : ICampaignClient
    {
        private readonly HttpClient _httpClient;

        public CampaignClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Response<NoContent>> CreateCampaign(CreateCampaignDto createCampaignDto)
        {
            var response = await _httpClient.PostAsJsonAsync("Campaigns", createCampaignDto);

            return await response.ReadFromJsonAsync<Response<NoContent>>();
        }

        public async Task<Response<NoContent>> UpdateCampaign(UpdateCampaignDto updateCampaignDto)
        {
            var response = await _httpClient.PutAsJsonAsync("Campaigns", updateCampaignDto);

            return await response.ReadFromJsonAsync<Response<NoContent>>();
        }

        public async Task<Response<List<CampaignViewModel>>> GetAllCampaign()
        {
            var response = await _httpClient.GetAsync("Campaigns");
            
            return await response.ReadFromJsonAsync<Response<List<CampaignViewModel>>>();
        }

        public async Task<Response<CampaignViewModel>> GetByCampaignId(int campaignId)
        {
            var response = await _httpClient.GetAsync($"Campaigns/{campaignId}");

            return await response.ReadFromJsonAsync<Response<CampaignViewModel>>();
        }

        public async Task<Response<CampaignViewModel>> GetByCampaignTypeAndDiscountType(string campaignType, string discountType)
        {
            var response = await _httpClient.GetAsync($"Campaigns/{campaignType}/{discountType}");
            
            return await response.ReadFromJsonAsync<Response<CampaignViewModel>>();
        }

        public async Task<Response<NoContent>> DeleteCampaign(int id)
        {
            var response = await _httpClient.DeleteAsync($"Campaigns/{id}");

           return await response.ReadFromJsonAsync<Response<NoContent>>();
        }
    }
}
