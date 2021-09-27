using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Shared.ControllerBase;
using Web.Client.Abstract;
using Web.Models.Dtos;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsController : CustomBaseController
    {
        private readonly ICampaignClient _campaignClient;

        public CampaignsController(ICampaignClient campaignClient)
        {
            _campaignClient = campaignClient;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCampaign(CreateCampaignDto createCampaignDto)
        {
            return CreateActionResultInstance(await _campaignClient.CreateCampaign(createCampaignDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCampaign(UpdateCampaignDto updateCampaignDto)
        {
            return CreateActionResultInstance(await _campaignClient.UpdateCampaign(updateCampaignDto));
        }

        [HttpGet("{campaignType}/{discountType}")]
        public async Task<IActionResult> GetByCampaignTypeAndDiscountType(string campaignType, string discountType)
        {
            return CreateActionResultInstance(await _campaignClient.GetByCampaignTypeAndDiscountType(campaignType, discountType));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCampaign()
        {
            return CreateActionResultInstance(await _campaignClient.GetAllCampaign());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCampaign(int id)
        {
            return CreateActionResultInstance(await _campaignClient.DeleteCampaign(id));
        }
    }
}
