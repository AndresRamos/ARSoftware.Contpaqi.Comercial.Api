using System.Text.Json;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Infrastructure.Persistence.Configurations;

public sealed class EmitDocumentoRequestConfiguration : IEntityTypeConfiguration<CreateDocumentoDigitalRequest>
{
    public void Configure(EntityTypeBuilder<CreateDocumentoDigitalRequest> builder)
    {
        builder.Property(m => m.Model)
            .HasConversion(p => JsonSerializer.Serialize(p, JsonSerializerOptions.Default),
                p => JsonSerializer.Deserialize<LlaveDocumento>(p, JsonSerializerOptions.Default)!)
            .HasColumnName("Model");

        builder.Property(m => m.Options)
            .HasConversion(p => JsonSerializer.Serialize(p, JsonSerializerOptions.Default),
                p => JsonSerializer.Deserialize<CreateDocumentoDigitalOptions>(p, JsonSerializerOptions.Default)!)
            .HasColumnName("Options");
    }
}

public sealed class EmitDocumentoResponseConfiguration : IEntityTypeConfiguration<CreateDocumentoDigitalResponse>
{
    public void Configure(EntityTypeBuilder<CreateDocumentoDigitalResponse> builder)
    {
        builder.Property(m => m.Model)
            .HasConversion(p => JsonSerializer.Serialize(p, JsonSerializerOptions.Default),
                p => JsonSerializer.Deserialize<DocumentoDigital>(p, JsonSerializerOptions.Default)!)
            .HasColumnName("Model");
    }
}
