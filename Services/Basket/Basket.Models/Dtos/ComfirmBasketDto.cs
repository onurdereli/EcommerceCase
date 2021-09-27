using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Messages.Events.Concrete;

namespace Basket.Models.Dtos
{
    public class ComfirmBasketDto
    {
        public string UserId { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal TotalDiscountPrice { get; set; }

        public decimal CargoPrice { get; set; }

        public decimal TotalDiscountCargoPrice { get; set; }

        public int UsedCampaignId { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public ComfirmBasketDto()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
