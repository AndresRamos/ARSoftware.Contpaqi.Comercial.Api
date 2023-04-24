using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.Common.Models;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using FluentValidation;

namespace Api.Sync.Core.Application.Requests.Agentes;

public sealed class ActualizarAgenteRequestValidator : AbstractValidator<ActualizarAgenteRequest>
{
    public ActualizarAgenteRequestValidator(IAgenteRepository agenteRepository)
    {
        RuleFor(m => m.Model.CodigoAgente)
            .MustAsync(async (s, token) => await agenteRepository.BuscarPorCodigoAsync(s, LoadRelatedDataOptions.Default, token) != null)
            .WithMessage("El cliente {PropertyValue} no existe.");
    }
}
