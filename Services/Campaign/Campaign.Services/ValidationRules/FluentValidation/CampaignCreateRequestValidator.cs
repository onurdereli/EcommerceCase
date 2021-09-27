using Campaign.Models.Requests;
using FluentValidation;

namespace Campaign.Services.ValidationRules.FluentValidation
{
    public class CampaignCreateRequestValidator : AbstractValidator<CampaignCreateRequest>
    {
        public CampaignCreateRequestValidator()
        {
            RuleFor(r => r.CampaignType).NotEmpty().WithMessage("Kampanya tipi boş olamaz.");
            RuleFor(r => r.DiscountType).NotEmpty().WithMessage("İndirim tipi boş olamaz.");
            RuleFor(r => r.DiscountValue).NotEmpty().WithMessage("İndirim değeri boş olamaz.");
            RuleFor(r => r.Threshold).NotEmpty().GreaterThan(0).WithMessage("Kampanya eşiği boş olamaz.");
        }
    }
}