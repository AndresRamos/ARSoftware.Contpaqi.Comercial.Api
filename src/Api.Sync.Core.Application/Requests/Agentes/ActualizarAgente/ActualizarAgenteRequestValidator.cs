using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using FluentValidation;

namespace Api.Sync.Core.Application.Requests.Agentes.ActualizarAgente;

public sealed class ActualizarAgenteRequestValidator : AbstractValidator<ActualizarAgenteRequest>
{
    public ActualizarAgenteRequestValidator(IAgenteRepository agenteRepository)
    {
        RuleFor(m => m.Model.CodigoAgente)
            .MustAsync(async (s, token) => await agenteRepository.ExistePorCodigoAsync(s, token))
            .WithMessage("El agente {PropertyValue} no existe.");
    }
}
