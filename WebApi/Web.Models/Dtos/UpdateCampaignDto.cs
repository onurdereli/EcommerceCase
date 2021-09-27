namespace Web.Models.Dtos
{
    public class UpdateCampaignDto
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
