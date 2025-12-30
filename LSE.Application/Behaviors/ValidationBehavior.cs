using FluentValidation;
using LSE.Application.Exceptions;
using MediatR;

namespace LSE.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var errors = _validators
                    .Select(v => v.Validate(context))
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (errors.Count > 0)
                {
                    var formattedErrors = errors
                                        .GroupBy(e => e.PropertyName)
                                        .ToDictionary(
                                            g => g.Key,
                                            g => g.Select(e => e.ErrorMessage).ToArray()
                                        );

                    // Throw a custom exception that middleware can catch
                    throw new CustomValidationException(formattedErrors);
                }
            }

            return await next();
        }
    }
}
