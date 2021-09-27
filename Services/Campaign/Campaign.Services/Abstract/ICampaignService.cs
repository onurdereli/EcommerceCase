using System.Collections.Generic;
using System.Threading.Tasks;
using Campaign.Models.Dtos;
using Campaign.Models.Requests;
using Shared.Dtos;

namespace Campaign.Services.Abstract
{
    public interface ICampaignService
    {
        Task<Response<List<CampaignDto>>> GetAll();

        Task<Response<CampaignDto>> GetByCampaignTypeAndDiscountType(string campaignType, string discountType);

        Task<Response<CampaignDto>> GetByCampaignId(int id);

        Task<Response<NoContent>> Save(CampaignCreateRequest campaignCreateRequest);

        Task<Response<NoContent>> Update(CampaignUpdateRequest campaignUpdateRequest);

        Task<Response<NoContent>> Delete(int id);
    }
}
