using System.Text.Json;
using Api.Core.Domain.Common;
using Api.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Persistence.Configurations;

public static class ApiConfigurationExtensions
{
    public static void Configure<TEntity, TModel, TOptions>(this ModelBuilder modelBuilder)
        where TEntity : class, IApiRequest<TModel, TOptions>
    {
        var jsonOptions = new JsonSerializerOptions(JsonSerializerOptions.Default) { TypeInfoResolver = new PolymorphicTypeResolver() };

        modelBuilder.Entity<TEntity>(builder =>
        {
            builder.Property(m => m.Model)
                .HasConversion(p => JsonSerializer.Serialize(p, jsonOptions), p => JsonSerializer.Deserialize<TModel>(p, jsonOptions)!)
                .HasColumnName(PersistanceConstants.ModelColumnName);

            builder.Property(m => m.Options)
                .HasConversion(p => JsonSerializer.Serialize(p, jsonOptions), p => JsonSerializer.Deserialize<TOptions>(p, jsonOptions)!)
                .HasColumnName(PersistanceConstants.OptionsColumnName);
        });
    }

    public static void Configure<TEntity, TModel>(this ModelBuilder modelBuilder) where TEntity : class, IApiResponse<TModel>
    {
        var jsonOptions = new JsonSerializerOptions(JsonSerializerOptions.Default) { TypeInfoResolver = new PolymorphicTypeResolver() };

        modelBuilder.Entity<TEntity>(builder =>
        {
            builder.Property(m => m.Model)
                .HasConversion(p => JsonSerializer.Serialize(p, jsonOptions), p => JsonSerializer.Deserialize<TModel>(p, jsonOptions)!)
                .HasColumnName(PersistanceConstants.ModelColumnName);
        });
    }
}
