using ARSoftware.Contpaqi.Api.Common.Domain;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Api.Sync.Core.Application.Common.Behaviours;

public sealed class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ContpaqiRequest, IRequest<TResponse> where TResponse : class
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            ValidationResult[] validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            List<ValidationFailure> failures = validationResults.Where(r => r.Errors.Any()).SelectMany(r => r.Errors).ToList();

            if (failures.Any())
            {
                var ex = new ValidationException(failures);
                var r = ApiResponse.CreateFailed(ex.Message);
                //return (TResponse)Convert.ChangeType(r, typeof(TResponse));
                return (r as TResponse)!;
            }
        }

        return await next();
    }
}
