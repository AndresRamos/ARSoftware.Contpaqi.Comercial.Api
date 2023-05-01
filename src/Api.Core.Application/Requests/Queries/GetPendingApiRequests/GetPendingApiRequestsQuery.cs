using Api.Core.Application.Common.Interfaces;
using ARSoftware.Contpaqi.Api.Common.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Core.Application.Requests.Queries.GetPendingApiRequests;

public sealed record GetPendingApiRequestsQuery(string EmpresaRfc, string SubscriptionKey) : IRequest<IEnumerable<ApiRequest>>;

public sealed class GetPendingApiRequestsQueryHandler : IRequestHandler<GetPendingApiRequestsQuery, IEnumerable<ApiRequest>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetPendingApiRequestsQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<ApiRequest>> Handle(GetPendingApiRequestsQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Requests.AsNoTracking()
            .Where(m => m.Status == RequestStatus.Pending && m.EmpresaRfc == request.EmpresaRfc &&
                        m.SubscriptionKey == request.SubscriptionKey)
            .ToListAsync(cancellationToken);
    }
}
