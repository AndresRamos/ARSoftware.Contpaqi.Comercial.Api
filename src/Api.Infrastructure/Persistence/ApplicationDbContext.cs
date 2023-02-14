﻿using System.Reflection;
using Api.Core.Application.Common.Interfaces;
using Api.Core.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ApiRequestBase> Requests => Set<ApiRequestBase>();
    public DbSet<ApiResponseBase> Responses => Set<ApiResponseBase>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApiRequestBase>().UseTphMappingStrategy().ToTable("Requests");
        builder.Entity<ApiResponseBase>().UseTphMappingStrategy().ToTable("Responses");

        builder.Entity<ApiRequestBase>().HasOne(r => r.Response).WithOne().HasForeignKey<ApiResponseBase>(r => r.Id);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
