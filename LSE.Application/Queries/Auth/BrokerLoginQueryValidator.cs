using FluentValidation; 

namespace LSE.Application.Queries.Auth
{
    public class BrokerLoginQueryValidator : AbstractValidator<BrokerLoginQuery>
    {
        public BrokerLoginQueryValidator()
        {
            // Username validation
            RuleFor(x => x.UserName)
                 .NotEmpty().WithMessage("Username is required.")
                 .MinimumLength(3).WithMessage("Username must be at least 3 characters.")
                 .MaximumLength(100).WithMessage("Username cannot exceed 100 characters.")
                 .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                 .WithMessage("Username must be a valid email address format.");

            // Password validation
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(4).WithMessage("Password must be at least 4 characters long.")
                .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.");
        }
    }
}
