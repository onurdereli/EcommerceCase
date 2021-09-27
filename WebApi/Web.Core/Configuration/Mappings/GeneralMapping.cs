using AutoMapper;
using Web.Models.Dtos;
using Web.Models.ViewModels;

namespace Web.Core.Configuration.Mappings
{
    public class GeneralMapping: Profile
    {
        public GeneralMapping()
        {
            CreateMap<BasketDto, BasketViewModel>()
                .ForMember(dto => dto.BasketItems, opt => opt.MapFrom(src => src.BasketItems))
                .ReverseMap();
            CreateMap<BasketItemDto, BasketItemViewModel>()
                .ReverseMap();
        }
    }
}
