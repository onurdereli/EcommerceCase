using AutoMapper;
using Basket.Models.Dtos;
using Shared.Messages.Events.Concrete;

namespace Basket.Core.Configuration.Mappings
{
    public class GeneralMapping: Profile
    {
        public GeneralMapping()
        {
            CreateMap<ComfirmBasketDto, BasketComfirmedEvent>()
                .ForMember(dto => dto.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
                .ReverseMap();
        }
    }
}
