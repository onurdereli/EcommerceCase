using System.Linq;
using Basket.Models.Dtos;
using FluentValidation;

namespace Basket.Services.ValidationRules.FluentValidation
{
    public class BasketDtoValidator : AbstractValidator<BasketDto>
    {
        public BasketDtoValidator()
        {
            RuleFor(r => r.UserId).NotEmpty().WithMessage("Lütfen kullanıcı girişi yapınız.");
            RuleFor(r => r.BasketItems).NotEmpty().WithMessage("Lütfen sepetinize ürün ekleyiniz.");
            RuleFor(r => r.BasketItems.Select(item => item.Price)).NotEmpty().WithMessage("Fiyat alanı boş olamaz.");
        }
    }
}
