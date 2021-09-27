using System.Collections.Generic;
using System.Linq;
using Shared.Entities;

namespace Basket.Models.Dtos
{
    public class BasketDto: IDto
    {
        public string UserId { get; set; }

        public List<BasketItemDto> BasketItems { get; set; }

        public decimal CargoPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal TotalDiscountPrice { get; set; }
        
        public decimal TotalDiscountCargoPrice { get; set; }

        public int UsedCampaignId { get; set; }

        public BasketDto()
        {
            BasketItems = new List<BasketItemDto>();
        }
    }
}
