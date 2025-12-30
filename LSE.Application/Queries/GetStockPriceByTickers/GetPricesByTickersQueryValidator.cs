using FluentValidation; 

namespace LSE.Application.Queries.GetStockPriceByTickers
{
    public class GetPricesByTickersQueryValidator : AbstractValidator<GetPricesByTickersQuery>
    {
        public GetPricesByTickersQueryValidator()
        {
            RuleFor(x => x.Tickers)
                .NotEmpty().WithMessage("Tickers list cannot be empty.")
                .Must(list => list.Count <= 50)
                .WithMessage("Maximum 50 tickers per request allowed.");

            RuleForEach(x => x.Tickers)
                .NotEmpty().WithMessage("Ticker cannot be empty.")
                .Length(2, 10).WithMessage("Ticker length must be between 2 and 10.")
                .Matches("^[A-Za-z0-9:.-]+$")
                    .WithMessage("Ticker contains invalid characters.");
        }
    }
}
