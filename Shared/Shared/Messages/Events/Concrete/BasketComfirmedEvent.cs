using System.Collections.Generic;
using Shared.Messages.Events.Abstract;

namespace Shared.Messages.Events.Concrete
{
    public class BasketComfirmedEvent: IBasketComfirmedEvent
    {
        public string UserId { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal TotalDiscountPrice { get; set; }

        public decimal CargoPrice { get; set; }

        public decimal TotalDiscountCargoPrice { get; set; }

        public int UsedCampaignId { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public BasketComfirmedEvent()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
