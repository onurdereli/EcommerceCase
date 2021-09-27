using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Order.Application.Commands.CreateOrder;
using Order.Domain.Entities;
using Shared.Messages.Events.Abstract;
using MassTransit;
using Microsoft.Extensions.Logging;
using IMediator = MediatR.IMediator;

namespace Order.API.Consumers
{
    public class BasketConfirmedEventConsumer: IConsumer<IBasketComfirmedEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<BasketConfirmedEventConsumer> _logger;


        public BasketConfirmedEventConsumer(IMediator mediator, IMapper mapper, ILogger<BasketConfirmedEventConsumer> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IBasketComfirmedEvent> context)
        {
            var command = new CreateOrderCommand
            {
                CargoPrice = context.Message.CargoPrice,
                TotalDiscountPrice = context.Message.TotalDiscountPrice,
                TotalDiscountCargoPrice = context.Message.TotalDiscountCargoPrice,
                TotalPrice = context.Message.TotalPrice,
                UserId = context.Message.UserId,
                UsedCampaignId = context.Message.UsedCampaignId
            };

            context.Message.OrderItems.ForEach(item=>
            {
                command.OrderItems.Add(new(item.ProductId, item.ProductCode, item.Price, item.Quantity));
            });

            var result = await _mediator.Send(command);

            _logger.LogInformation("BasketComfirmedEvent consumed successfully. Order Id : {newOrderId}", result.Data.Id);
        }
    }
}
