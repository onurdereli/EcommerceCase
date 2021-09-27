using AutoMapper;
using Order.Application.Commands.CreateOrder;
using Order.Application.Responses;
using Shared.Messages.Events.Abstract;
using Shared.Messages.Events.Concrete;

namespace Order.Application.Configuration.Mappings
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Domain.Entities.Order, CreateOrderCommand>()
                .ReverseMap();
            CreateMap<Domain.Entities.Order, OrderCreateResponse>()
                .ReverseMap();
            CreateMap<Domain.Entities.Order, OrderByUserIdQueryResponse>()
                .ReverseMap();
        }
    }
}
