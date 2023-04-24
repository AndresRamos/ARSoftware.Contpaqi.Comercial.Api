using Api.Core.Application.Validators.Agentes;
using FluentValidation;

namespace Api.Core.Application.Requests.Commands.CreateApiRequest;

public sealed class RequestValidator : AbstractValidator<CreateApiRequestCommand>
{
    public RequestValidator()
    {
        RuleFor(x => x.ContpaqiRequest)
            .SetInheritanceValidator(v =>
            {
                v.Add(new CrearAgenteRequestValidator());
                v.Add(new ActualizarAgenteRequestValidator());
            });
    }
}
