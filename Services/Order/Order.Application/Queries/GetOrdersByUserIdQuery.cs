using System.Collections.Generic;
using Order.Application.Responses;
using Shared.Dtos;
using MediatR;

namespace Order.Application.Queries
{
    public class GetOrdersByUserIdQuery : IRequest<Response<IEnumerable<OrderByUserIdQueryResponse>>>
    {
        public string UserId { get; set; }

        public GetOrdersByUserIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}
