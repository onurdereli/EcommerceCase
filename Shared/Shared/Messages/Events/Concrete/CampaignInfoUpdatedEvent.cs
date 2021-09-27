using Shared.Messages.Events.Abstract;

namespace Shared.Messages.Events.Concrete
{
    public class CampaignInfoUpdatedEvent : ICampaignInfoUpdatedEvent
    { 
        public int Id { get; set; }

        public int Threshold { get; set; }

        public decimal UsedAmount { get; set; }
    }
}
