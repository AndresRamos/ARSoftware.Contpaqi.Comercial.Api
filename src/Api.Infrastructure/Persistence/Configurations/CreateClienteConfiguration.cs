using System.Text.Json;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Infrastructure.Persistence.Configurations;

public sealed class CreateClienteRequestConfiguration : IEntityTypeConfiguration<CreateClienteRequest>
{
    public void Configure(EntityTypeBuilder<CreateClienteRequest> builder)
    {
        builder.Property(m => m.Model)
            .HasConversion(p => JsonSerializer.Serialize(p, JsonSerializerOptions.Default),
                p => JsonSerializer.Deserialize<Cliente>(p, JsonSerializerOptions.Default)!)
            .HasColumnName("Model");

        builder.Property(m => m.Options)
            .HasConversion(p => JsonSerializer.Serialize(p, JsonSerializerOptions.Default),
                p => JsonSerializer.Deserialize<CreateClienteOptions>(p, JsonSerializerOptions.Default)!)
            .HasColumnName("Options");
    }
}

public sealed class CrateClienteResponseConfiguration : IEntityTypeConfiguration<CreateClienteResponse>
{
    public void Configure(EntityTypeBuilder<CreateClienteResponse> builder)
    {
        builder.Property(m => m.Model)
            .HasConversion(p => JsonSerializer.Serialize(p, JsonSerializerOptions.Default),
                p => JsonSerializer.Deserialize<Cliente>(p, JsonSerializerOptions.Default)!)
            .HasColumnName("Model");
    }
}
