using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messages.Events.Abstract
{
    public interface ICampaignInfoUpdatedEvent
    {
        public int Id { get; set; }

        public int Threshold { get; set; }

        public decimal UsedAmount { get; set; }
    }
}
