using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using FluentValidation;

namespace Api.Sync.Core.Application.Requests.Almacenes.CrearAlmacen;

public sealed class CrearAlmacenRequestValidator : AbstractValidator<CrearAlmacenRequest>
{
    public CrearAlmacenRequestValidator(IAlmacenRepository almacenRepository)
    {
        RuleFor(m => m.Model.Almacen.Codigo)
            .MustAsync(async (s, token) => await almacenRepository.ExistePorCodigoAsync(s, token) == false)
            .WithMessage("El almacen {PropertyValue} ya existe.");
    }
}
