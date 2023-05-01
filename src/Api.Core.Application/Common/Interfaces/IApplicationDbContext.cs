using ARSoftware.Contpaqi.Api.Common.Domain;
using Microsoft.EntityFrameworkCore;

namespace Api.Core.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ApiRequest> Requests { get; }
    DbSet<ApiResponse> Responses { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
