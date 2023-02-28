using Api.Core.Application.Common.Interfaces;
using Api.Core.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Core.Application.Requests.Queries.GetApiRequests;

public sealed record GetApiRequestsQuery(DateOnly StartDate, DateOnly EndDate) : IRequest<IEnumerable<ApiRequestBase>>
{
}

public sealed class GetApiRequestsQueryHandler : IRequestHandler<GetApiRequestsQuery, IEnumerable<ApiRequestBase>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetApiRequestsQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<ApiRequestBase>> Handle(GetApiRequestsQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Requests.AsNoTracking()
            .Include(m => m.Response)
            .Where(m => EF.Functions.DateDiffDay(m.DateCreated, request.StartDate.ToDateTime(TimeOnly.MinValue)) >= 0 &&
                        EF.Functions.DateDiffDay(m.DateCreated, request.StartDate.ToDateTime(TimeOnly.MaxValue)) <= 0)
            .OrderBy(m => m.DateCreated)
            .ToListAsync(cancellationToken);
    }
}
