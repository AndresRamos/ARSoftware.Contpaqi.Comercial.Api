using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using FluentValidation;

namespace Api.Sync.Core.Application.Requests.Documentos.SaldarDocumento;

public sealed class SaldarDocumentoRequestValidator : AbstractValidator<SaldarDocumentoRequest>
{
    public SaldarDocumentoRequestValidator(IDocumentoRepository documentoRepository)
    {
        RuleFor(m => m.Model.DocumentoAPagar)
            .MustAsync(async (documento, token) => await documentoRepository.ExistePorLlaveAsync(documento, token))
            .WithMessage("El documento {PropertyValue} no existe.");

        RuleFor(m => m.Model.DocumentoPago)
            .MustAsync(async (documento, token) => await documentoRepository.ExistePorLlaveAsync(documento, token))
            .WithMessage("El documento {PropertyValue} no existe.");
    }
}
