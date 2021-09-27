using Order.Application.Commands.CreateOrder;
using FluentValidation;

namespace Order.Application.ValidationRules.FluentValidation
{
    public class OrderCreateValidator : AbstractValidator<CreateOrderCommand>
    {
        public OrderCreateValidator()
        {
            RuleFor(v => v.CargoPrice)
                .NotEmpty();

            RuleFor(v => v.TotalPrice)
                .NotEmpty();
        }
    }
}
