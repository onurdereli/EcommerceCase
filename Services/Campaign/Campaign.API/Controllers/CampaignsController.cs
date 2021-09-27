using System.Threading.Tasks;
using Campaign.Models.Dtos;
using Campaign.Models.Requests;
using Campaign.Services.Abstract;
using Shared.ControllerBase;
using Microsoft.AspNetCore.Mvc;

namespace Campaign.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsController : CustomBaseController
    {
        private readonly ICampaignService _campaignService;

        public CampaignsController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _campaignService.GetAll());
        }

        [HttpGet("{campaignType}/{discountType}")]
        public async Task<IActionResult> GetByCampaignTypeAndDiscountType(string campaignType, string discountType)
        {
            return CreateActionResultInstance(await _campaignService.GetByCampaignTypeAndDiscountType(campaignType, discountType));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByCampaignId(int id)
        {
            return CreateActionResultInstance(await _campaignService.GetByCampaignId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CampaignCreateRequest campaignCreateRequest)
        {
            return CreateActionResultInstance(await _campaignService.Save(campaignCreateRequest));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CampaignUpdateRequest campaignUpdateRequest)
        {
            return CreateActionResultInstance(await _campaignService.Update(campaignUpdateRequest));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResultInstance(await _campaignService.Delete(id));
        }
    }
}