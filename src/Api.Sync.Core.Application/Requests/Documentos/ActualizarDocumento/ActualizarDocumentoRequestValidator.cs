using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using FluentValidation;

namespace Api.Sync.Core.Application.Requests.Documentos.ActualizarDocumento;

public sealed class ActualizarDocumentoRequestValidator : AbstractValidator<ActualizarDocumentoRequest>
{
    public ActualizarDocumentoRequestValidator(IDocumentoRepository documentoRepository)
    {
        RuleFor(m => m.Model.LlaveDocumento)
            .MustAsync(async (documento, token) => await documentoRepository.ExistePorLlaveAsync(documento, token))
            .WithMessage("El documento {PropertyValue} no existe.");
    }
}
