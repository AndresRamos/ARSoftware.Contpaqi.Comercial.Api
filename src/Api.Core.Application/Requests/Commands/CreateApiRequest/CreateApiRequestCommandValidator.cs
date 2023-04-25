using Api.Core.Application.Validators.Agentes;
using Api.Core.Application.Validators.Almacenes;
using Api.Core.Application.Validators.Clientes;
using Api.Core.Application.Validators.Documentos;
using Api.Core.Application.Validators.Productos;
using FluentValidation;

namespace Api.Core.Application.Requests.Commands.CreateApiRequest;

public sealed class RequestValidator : AbstractValidator<CreateApiRequestCommand>
{
    public RequestValidator()
    {
        RuleFor(x => x.ContpaqiRequest)
            .SetInheritanceValidator(v =>
            {
                // Agentes
                v.Add(new CrearAgenteRequestValidator());
                v.Add(new ActualizarAgenteRequestValidator());

                // Almacenes
                v.Add(new ActualizarAlmacenRequestValidator());
                v.Add(new CrearAlmacenRequestValidator());

                // Clientes
                v.Add(new ActualizarClienteRequestValidator());
                v.Add(new CrearClienteRequestValidator());
                v.Add(new EliminarClienteRequestValidator());

                // Productos
                v.Add(new ActualizarProductoRequestValidator());
                v.Add(new CrearProductoRequestValidator());
                v.Add(new EliminarProductoRequestValidator());

                // Documentos
                v.Add(new ActualizarDocumentoRequestValidator());
                v.Add(new CancelarDocumentoRequestValidator());
                v.Add(new CrearDocumentoRequestValidator());
                v.Add(new EliminarDocumentoRequestValidator());
                v.Add(new GenerarDocumentoDigitalRequestValidator());
                v.Add(new SaldarDocumentoRequestValidator());
                v.Add(new TimbrarDocumentoRequestValidator());
            });
    }
}
