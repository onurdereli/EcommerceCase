using Shared.Entities;

namespace Campaign.Models.Entities
{
    [Dapper.Contrib.Extensions.Table("campaign")]
    public class Campaign : IEntity
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
