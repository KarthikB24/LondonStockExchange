using FluentValidation;
using LSE.Application.Queries.GetLatestPrice; 

namespace LSE.Application.Queries.GetLatestPricePerStock
{
    public class GetLatestPriceQueryValidator : AbstractValidator<GetLatestPriceQuery>
    {
        public GetLatestPriceQueryValidator()
        {
            // ticker is required
            RuleFor(x => x.Ticker)
                .NotEmpty()
                .WithMessage("Ticker is required.")
                .Must(t => !string.IsNullOrWhiteSpace(t))
                .WithMessage("Ticker cannot be whitespace.")
                .Matches(@"^[A-Za-z]+:[A-Za-z]+$")
                .WithMessage("Ticker must follow format 'EXCHANGE:SYMBOL' (example: LSE:VOD).");

            
        }
    }
}
