using System.Text.Json;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Infrastructure.Persistence.Configurations;

public sealed class CreateDocumentoRequestConfiguration : IEntityTypeConfiguration<CreateDocumentoRequest>
{
    public void Configure(EntityTypeBuilder<CreateDocumentoRequest> builder)
    {
        builder.Property(m => m.Model)
            .HasConversion(p => JsonSerializer.Serialize(p, JsonSerializerOptions.Default),
                p => JsonSerializer.Deserialize<Documento>(p, JsonSerializerOptions.Default)!)
            .HasColumnName("Model");

        builder.Property(m => m.Options)
            .HasConversion(p => JsonSerializer.Serialize(p, JsonSerializerOptions.Default),
                p => JsonSerializer.Deserialize<CreateDocumentoOptions>(p, JsonSerializerOptions.Default)!)
            .HasColumnName("Options");
    }
}

public sealed class CreateDocumentoResponseConfiguration : IEntityTypeConfiguration<CreateDocumentoResponse>
{
    public void Configure(EntityTypeBuilder<CreateDocumentoResponse> builder)
    {
        builder.Property(m => m.Model)
            .HasConversion(p => JsonSerializer.Serialize(p, JsonSerializerOptions.Default),
                p => JsonSerializer.Deserialize<Documento>(p, JsonSerializerOptions.Default)!)
            .HasColumnName("Model");
    }
}
