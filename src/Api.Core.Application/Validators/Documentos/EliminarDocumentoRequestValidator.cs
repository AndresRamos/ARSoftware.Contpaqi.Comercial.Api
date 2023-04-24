using Api.Core.Domain.Requests;
using FluentValidation;

namespace Api.Core.Application.Validators.Documentos;

public sealed class EliminarDocumentoRequestValidator : AbstractValidator<EliminarDocumentoRequest>
{
    public EliminarDocumentoRequestValidator()
    {
        RuleFor(m => m.Model.LlaveDocumento.ConceptoCodigo).NotEmpty();
        RuleFor(m => m.Model.LlaveDocumento.Folio).NotEmpty();
    }
}
