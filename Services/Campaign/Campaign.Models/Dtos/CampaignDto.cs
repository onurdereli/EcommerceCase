using Shared.Entities;

namespace Campaign.Models.Dtos
{
    public class CampaignDto : IDto
    {
        public int Id { get; set; }

        public string CampaignType { get; set; }

        public string CampaignName { get; set; }

        public int Threshold { get; set; }

        public decimal UsedAmount { get; set; }

        public string DiscountType { get; set; }

        public decimal DiscountValue { get; set; }
    }
}
