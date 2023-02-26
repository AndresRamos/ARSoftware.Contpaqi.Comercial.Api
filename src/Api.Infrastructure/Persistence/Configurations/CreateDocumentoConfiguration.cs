using System.Text.Json;
using Api.Core.Domain.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Infrastructure.Persistence.Configurations;

public sealed class CreateDocumentoRequestConfiguration : IEntityTypeConfiguration<CrearDocumentoRequest>
{
    public void Configure(EntityTypeBuilder<CrearDocumentoRequest> builder)
    {
        builder.Property(m => m.Model)
            .HasConversion(p => JsonSerializer.Serialize(p, JsonSerializerOptions.Default),
                p => JsonSerializer.Deserialize<CrearDocumentoRequestModel>(p, JsonSerializerOptions.Default)!)
            .HasColumnName("Model");

        builder.Property(m => m.Options)
            .HasConversion(p => JsonSerializer.Serialize(p, JsonSerializerOptions.Default),
                p => JsonSerializer.Deserialize<CrearDocumentoRequestOptions>(p, JsonSerializerOptions.Default)!)
            .HasColumnName("Options");
    }
}

public sealed class CreateDocumentoResponseConfiguration : IEntityTypeConfiguration<CrearDocumentoResponse>
{
    public void Configure(EntityTypeBuilder<CrearDocumentoResponse> builder)
    {
        builder.Property(m => m.Model)
            .HasConversion(p => JsonSerializer.Serialize(p, JsonSerializerOptions.Default),
                p => JsonSerializer.Deserialize<CrearDocumentoResponseModel>(p, JsonSerializerOptions.Default)!)
            .HasColumnName("Model");
    }
}
