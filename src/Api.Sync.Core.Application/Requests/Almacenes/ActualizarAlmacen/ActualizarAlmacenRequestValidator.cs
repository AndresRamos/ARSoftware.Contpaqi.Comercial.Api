using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using FluentValidation;

namespace Api.Sync.Core.Application.Requests.Almacenes.ActualizarAlmacen;

public sealed class ActualizarAlmacenRequestValidator : AbstractValidator<ActualizarAlmacenRequest>
{
    public ActualizarAlmacenRequestValidator(IAlmacenRepository almacenRepository)
    {
        RuleFor(m => m.Model.CodigoAlmacen)
            .MustAsync(async (s, token) => await almacenRepository.ExistePorCodigoAsync(s, token))
            .WithMessage("El almacen {PropertyValue} no existe.");
    }
}
