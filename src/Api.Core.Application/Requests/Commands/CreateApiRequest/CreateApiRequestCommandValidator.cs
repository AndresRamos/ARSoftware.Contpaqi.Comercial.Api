using Api.Core.Application.Requests.Validators;
using FluentValidation;

namespace Api.Core.Application.Requests.Commands.CreateApiRequest;

public sealed class RequestValidator : AbstractValidator<CreateApiRequestCommand>
{
    public RequestValidator()
    {
        RuleFor(x => x.ApiRequest)
            .SetInheritanceValidator(v =>
            {
                v.Add(new CrearClienteRequestValidator());
                v.Add(new CreateDocumentoRequestValidator());
            });
    }
}
