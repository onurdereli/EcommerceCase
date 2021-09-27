﻿namespace Campaign.Models.Requests
{
    public class CampaignUpdateRequest
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
