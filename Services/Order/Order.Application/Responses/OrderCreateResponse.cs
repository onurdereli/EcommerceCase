using System;

namespace Order.Application.Responses
{
    public class OrderCreateResponse
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal TotalDiscountPrice { get; set; }

        public decimal CargoPrice { get; set; }

        public decimal TotalDiscountCargoPrice { get; set; }
    }
}
