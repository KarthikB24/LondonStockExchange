using FluentValidation; 

namespace LSE.Application.Commands.Trades
{
    public class AddTradeCommandValidator : AbstractValidator<AddTradeCommand>
    {
        public AddTradeCommandValidator()
        {
            RuleFor(x => x.Ticker)
                .NotEmpty().WithMessage("Ticker is required.")
                .Length(2, 10);

            RuleFor(x => x.BrokerCode)
                .NotEmpty().WithMessage("BrokerId is required.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

            RuleFor(x => x.Side)
                .Must(x => x == "BUY" || x == "SELL")
                .WithMessage("Side must be either BUY or SELL.");
        }
    }
}
