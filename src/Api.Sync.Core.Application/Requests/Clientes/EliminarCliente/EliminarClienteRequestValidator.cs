using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using FluentValidation;

namespace Api.Sync.Core.Application.Requests.Clientes.EliminarCliente;

public sealed class EliminarClienteRequestValidator : AbstractValidator<EliminarClienteRequest>
{
    public EliminarClienteRequestValidator(IClienteRepository clienteRepository)
    {
        RuleFor(m => m.Model.CodigoCliente)
            .MustAsync(async (s, token) => await clienteRepository.ExistePorCodigoAsync(s, token))
            .WithMessage("El cliente {PropertyValue} no existe.");
    }
}
