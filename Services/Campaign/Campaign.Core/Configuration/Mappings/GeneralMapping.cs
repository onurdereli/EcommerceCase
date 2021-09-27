using AutoMapper;
using Campaign.Models.Dtos;
using Campaign.Models.Requests;

namespace Campaign.Core.Configuration.Mappings
{
    public class GeneralMapping: Profile
    {
        public GeneralMapping()
        {
            CreateMap<Models.Entities.Campaign, CampaignDto>()
                .ReverseMap();
            CreateMap<Models.Entities.Campaign, CampaignCreateRequest>()
                .ReverseMap();
            CreateMap<CampaignDto, CampaignUpdateRequest>()
                .ReverseMap();
            CreateMap<Models.Entities.Campaign, CampaignUpdateRequest>()
                .ReverseMap();
        }
    }
}
