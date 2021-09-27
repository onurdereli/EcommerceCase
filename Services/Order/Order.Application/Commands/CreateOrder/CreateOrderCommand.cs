using System;
using System.Collections.Generic;
using Order.Application.Responses;
using Order.Domain.Entities;
using Shared.Dtos;
using MediatR;

namespace Order.Application.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Response<OrderCreateResponse>>
    {
        public string UserId { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal TotalDiscountPrice { get; set; }

        public decimal CargoPrice { get; set; }

        public decimal TotalDiscountCargoPrice { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public int UsedCampaignId { get; set; }

        public CreateOrderCommand()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
