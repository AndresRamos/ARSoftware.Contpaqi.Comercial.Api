using Api.Core.Domain.Requests;
using FluentValidation;

namespace Api.Core.Application.Validators.Documentos;

public sealed class TimbrarDocumentoRequestValidator : AbstractValidator<TimbrarDocumentoRequest>
{
    public TimbrarDocumentoRequestValidator()
    {
        RuleFor(m => m.Model.LlaveDocumento.ConceptoCodigo).NotEmpty();
        RuleFor(m => m.Model.LlaveDocumento.Folio).NotEmpty();
        RuleFor(m => m.Model.ContrasenaCertificado).NotEmpty();
    }
}
