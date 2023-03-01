using Api.Core.Application.Common.Interfaces;
using Api.Core.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Core.Application.Requests.Queries.GetPendingApiRequests;

public sealed record GetPendingApiRequestsQuery(string SubscriptionKey) : IRequest<IEnumerable<ApiRequestBase>>;

public sealed class GetPendingApiRequestsQueryHandler : IRequestHandler<GetPendingApiRequestsQuery, IEnumerable<ApiRequestBase>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetPendingApiRequestsQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<ApiRequestBase>> Handle(GetPendingApiRequestsQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Requests.AsNoTracking()
            .Where(m => m.Status == RequestStatus.Pending && m.SubscriptionKey == request.SubscriptionKey)
            .ToListAsync(cancellationToken);
    }
}
