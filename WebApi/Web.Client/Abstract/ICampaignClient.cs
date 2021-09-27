using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Dtos;
using Web.Models.Dtos;
using Web.Models.ViewModels;

namespace Web.Client.Abstract
{
    public interface ICampaignClient
    {
        Task<Response<NoContent>> CreateCampaign(CreateCampaignDto createCampaignDto);

        Task<Response<NoContent>> UpdateCampaign(UpdateCampaignDto updateCampaignDto);

        Task<Response<List<CampaignViewModel>>> GetAllCampaign();

        Task<Response<CampaignViewModel>> GetByCampaignId(int campaignId);

        Task<Response<CampaignViewModel>> GetByCampaignTypeAndDiscountType(string campaignType, string discountType);

        Task<Response<NoContent>> DeleteCampaign(int id);
    }
}
