using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Order.Application.Queries;
using Order.Application.Responses;
using Order.Domain.Repositories;
using Shared.Dtos;
using MediatR;

namespace Order.Application.Handlers
{
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<IEnumerable<OrderByUserIdQueryResponse>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersByUserIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<OrderByUserIdQueryResponse>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersByUserId(request.UserId);
            
            var orderResponses = _mapper.Map<IEnumerable<OrderByUserIdQueryResponse>>(orders);

            return Response<IEnumerable<OrderByUserIdQueryResponse>>.Success(orderResponses, 200);
        }
    }
}
