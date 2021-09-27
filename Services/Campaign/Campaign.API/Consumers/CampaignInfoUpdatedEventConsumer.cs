using System.Threading.Tasks;
using AutoMapper;
using Campaign.Models.Requests;
using Campaign.Services.Abstract;
using Shared.Messages.Events.Abstract;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Campaign.API.Consumers
{
    public class CampaignInfoUpdatedEventConsumer: IConsumer<ICampaignInfoUpdatedEvent>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CampaignInfoUpdatedEventConsumer> _logger;
        private readonly ICampaignService _campaignService;

        public CampaignInfoUpdatedEventConsumer(IMapper mapper, ILogger<CampaignInfoUpdatedEventConsumer> logger, ICampaignService campaignService)
        {
            _mapper = mapper;
            _logger = logger;
            _campaignService = campaignService;
        }

        public async Task Consume(ConsumeContext<ICampaignInfoUpdatedEvent> context)
        {
            var campaignResponse = await _campaignService.GetByCampaignId(context.Message.Id);
            
            if(campaignResponse.IsSuccessfull)
            {
                campaignResponse.Data.Threshold -= context.Message.Threshold;
                campaignResponse.Data.UsedAmount += context.Message.UsedAmount;
            }

            await _campaignService.Update(_mapper.Map<CampaignUpdateRequest>(campaignResponse.Data));

            _logger.LogInformation("CampaignInfoUpdatedEventConsumer consumed successfully.");
        }
    }
}
