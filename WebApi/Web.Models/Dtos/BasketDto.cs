using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.Dtos
{
    public class BasketDto
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
