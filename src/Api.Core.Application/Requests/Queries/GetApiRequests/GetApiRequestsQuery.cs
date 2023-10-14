using Api.Core.Application.Common.Interfaces;
using ARSoftware.Contpaqi.Api.Common.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Core.Application.Requests.Queries.GetApiRequests;

public sealed record GetApiRequestsQuery(DateOnly StartDate, DateOnly EndDate, string SubscriptionKey) : IRequest<IEnumerable<ApiRequest>>;

public sealed class GetApiRequestsQueryHandler : IRequestHandler<GetApiRequestsQuery, IEnumerable<ApiRequest>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetApiRequestsQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public Task<IEnumerable<ApiRequest>> Handle(GetApiRequestsQuery request, CancellationToken cancellationToken)
    {
        // Hay un error o bug que afecta el rendimiento de la consulta se hace utilizando los metodos asincronos
        // Leer: https://github.com/dotnet/efcore/issues/18221 y https://github.com/dotnet/SqlClient/issues/245 y https://github.com/dotnet/SqlClient/issues/593
        // Todo: Convertir a async cuando EF Core libere una una version que corrija el rendimiento
        return Task.FromResult<IEnumerable<ApiRequest>>(_applicationDbContext.Requests.AsNoTracking()
            .Include(m => m.Response)
            .Where(m => EF.Functions.DateDiffDay(m.DateCreated, request.StartDate.ToDateTime(TimeOnly.MinValue)) >= 0 &&
                        EF.Functions.DateDiffDay(m.DateCreated, request.EndDate.ToDateTime(TimeOnly.MaxValue)) <= 0 &&
                        m.SubscriptionKey == request.SubscriptionKey)
            .OrderBy(m => m.DateCreated)
            .ToList());
    }
}