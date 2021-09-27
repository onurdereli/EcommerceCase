using System;
using System.Collections.Generic;
using System.Linq;
using Shared.Constants;

namespace Web.Models.ViewModels
{
    public class BasketViewModel
    {
        public string UserId { get; set; }
        
        public List<BasketItemViewModel> BasketItems { get; set; }
        
        public decimal TotalPrice => BasketItems.Sum(x => x.Price * x.Quantity);

        public decimal TotalDiscountPrice { get; set; }

        public decimal CargoPrice => (decimal) EcommerceConst.FixedCargoCost * BasketItems.Sum(x=> x.Quantity);

        public decimal TotalDiscountCargoPrice { get; set; }

        public bool HasCampaignAdded => TotalDiscountCargoPrice > 0 || TotalDiscountPrice > 0;

        public int UsedCampaignId { get; set; }

        public BasketViewModel()
        {
            BasketItems = new List<BasketItemViewModel>();
        }

        public void RemoveCampaign()
        {
            TotalDiscountCargoPrice = 0;
            TotalDiscountPrice = 0;
        }
    }
}