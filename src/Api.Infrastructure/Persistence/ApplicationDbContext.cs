using System.Text.Json;
using Api.Core.Application.Common.Interfaces;
using Api.Core.Domain.Common;
using ARSoftware.Contpaqi.Api.Common.Domain;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ApiRequest> Requests => Set<ApiRequest>();
    public DbSet<ApiResponse> Responses => Set<ApiResponse>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApiRequest>().UseTphMappingStrategy();
        modelBuilder.Entity<ApiResponse>().UseTphMappingStrategy();

        modelBuilder.Entity<ApiRequest>().HasOne(r => r.Response).WithOne().HasForeignKey<ApiResponse>(r => r.Id);

        modelBuilder.Entity<ApiRequest>()
            .Property(e => e.ContpaqiRequest)
            .HasConversion(v => JsonSerializer.Serialize(v, JsonExtensions.GetJsonSerializerOptions()),
                v => JsonSerializer.Deserialize<ContpaqiRequest>(v, JsonExtensions.GetJsonSerializerOptions())!);

        modelBuilder.Entity<ApiResponse>()
            .Property(e => e.ContpaqiResponse)
            .HasConversion(v => JsonSerializer.Serialize(v, JsonExtensions.GetJsonSerializerOptions()),
                v => JsonSerializer.Deserialize<ContpaqiResponse>(v, JsonExtensions.GetJsonSerializerOptions())!);

        base.OnModelCreating(modelBuilder);
    }
}
