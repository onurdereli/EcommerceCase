using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Order.Application.Commands.CreateOrder;
using Order.Application.Responses;
using Order.Domain.Repositories;
using Order.Instrastructure.Data;
using Shared.Messages.Events.Abstract;
using Shared.Messages.Events.Concrete;
using MassTransit;
using MediatR;

namespace Order.Application.Handlers
{
    public class OrderCreateCommandHandler : IRequestHandler<CreateOrderCommand, Shared.Dtos.Response<OrderCreateResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderCreateCommandHandler(IOrderRepository orderRepository, IMapper mapper, IPublishEndpoint publishEndpoint) 
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Shared.Dtos.Response<OrderCreateResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = new Domain.Entities.Order(request.UserId, request.TotalPrice, request.TotalDiscountPrice, request.CargoPrice, request.TotalDiscountCargoPrice);

            request.OrderItems.ForEach(item=>
            {
                orderEntity.AddOrderItem(item.ProductId, item.ProductCode, item.Price, item.Quantity);
            });
            
            var order = await _orderRepository.AddAsync(orderEntity);

            var orderResponse = _mapper.Map<OrderCreateResponse>(order);

            foreach (var orderItem in order.OrderItems)
            {
                await _publishEndpoint.Publish<IStockQuantityReducedEvent>(new StockQuantityReducedEvent
                {
                    Id = orderItem.ProductId,
                    Code = orderItem.ProductCode,
                    Price = orderItem.Price,
                    Quantity = orderItem.Quantity
                }, cancellationToken);
            }

            await _publishEndpoint.Publish<ICampaignInfoUpdatedEvent>(new CampaignInfoUpdatedEvent
            {
                Id = request.UsedCampaignId,
                Threshold = order.OrderItems.Sum(x=> x.Quantity),
                UsedAmount = order.GetUsedAmount()
            }, cancellationToken);
            
            return Shared.Dtos.Response<OrderCreateResponse>.Success(orderResponse, 200);
        }
    }
}