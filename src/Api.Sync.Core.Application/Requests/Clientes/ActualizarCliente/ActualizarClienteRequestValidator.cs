using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using FluentValidation;

namespace Api.Sync.Core.Application.Requests.Clientes.ActualizarCliente;

public sealed class ActualizarClienteRequestValidator : AbstractValidator<ActualizarClienteRequest>
{
    public ActualizarClienteRequestValidator(IClienteRepository clienteRepository)
    {
        RuleFor(m => m.Model.CodigoCliente)
            .MustAsync(async (s, token) => await clienteRepository.ExistePorCodigoAsync(s, token))
            .WithMessage("El cliente {PropertyValue} no existe.");
    }
}
