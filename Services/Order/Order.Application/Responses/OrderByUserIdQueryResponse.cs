using System;
using System.Collections.Generic;
using Order.Domain.Entities;

namespace Order.Application.Responses
{
    public class OrderByUserIdQueryResponse
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public decimal? TotalPrice { get; set; }

        public decimal? TotalDiscountPrice { get; set; }

        public decimal? CargoPrice { get; set; }

        public decimal? TotalDiscountCargoPrice { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public OrderByUserIdQueryResponse()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
