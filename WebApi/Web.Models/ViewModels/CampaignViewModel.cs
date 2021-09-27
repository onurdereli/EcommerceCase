using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.ViewModels
{
    public class CampaignViewModel
    {
        public int Id { get; set; }

        public string CampaignType { get; set; }

        public string Name { get; set; }

        public int Threshold { get; set; }

        public decimal UsedAmount { get; set; }

        public string DiscountType { get; set; }

        public decimal DiscountValue { get; set; }
    }
}
