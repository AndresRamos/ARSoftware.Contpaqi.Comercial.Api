using Api.Core.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Api.Core.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ApiRequestBase> Requests { get; }

    DbSet<ApiResponseBase> Responses { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
