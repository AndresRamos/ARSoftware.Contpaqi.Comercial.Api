using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using FluentValidation;

namespace Api.Sync.Core.Application.Requests.Agentes.CrearAgente;

public sealed class CrearAgenteRequestValidator : AbstractValidator<CrearAgenteRequest>
{
    public CrearAgenteRequestValidator(IAgenteRepository agenteRepository)
    {
        RuleFor(m => m.Model.Agente.Codigo)
            .MustAsync(async (s, token) => await agenteRepository.ExistePorCodigoAsync(s, token) == false)
            .WithMessage("El cliente {PropertyValue} ya existe.");
    }
}
