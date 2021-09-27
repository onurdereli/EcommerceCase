using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.ViewModels
{
    public class BasketItemViewModel
    {
        public string ProductId { get; set; }

        public string ProductCode { get; set; }

        public int Quantity { get; set; }
        
        public decimal Price { get; set; }

        public decimal? DiscountAppliedPrice;
        
        public decimal CurrentPrice => DiscountAppliedPrice ?? Price;

        public void AppliedDiscount(decimal discountPrice)
        {
            DiscountAppliedPrice = discountPrice;
        }
    }
}
