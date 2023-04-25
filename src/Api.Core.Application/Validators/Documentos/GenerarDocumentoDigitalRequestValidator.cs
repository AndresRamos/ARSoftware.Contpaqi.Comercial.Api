using Api.Core.Domain.Requests;
using FluentValidation;

namespace Api.Core.Application.Validators.Documentos;

public sealed class GenerarDocumentoDigitalRequestValidator : AbstractValidator<GenerarDocumentoDigitalRequest>
{
    public GenerarDocumentoDigitalRequestValidator()
    {
        RuleFor(m => m.Model.LlaveDocumento.ConceptoCodigo).NotEmpty();
        RuleFor(m => m.Model.LlaveDocumento.Folio).NotEmpty();
    }
}
