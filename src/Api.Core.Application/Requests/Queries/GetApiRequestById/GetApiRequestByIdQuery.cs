using Api.Core.Application.Common.Interfaces;
using ARSoftware.Contpaqi.Api.Common.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Core.Application.Requests.Queries.GetApiRequestById;

public sealed record GetApiRequestByIdQuery(Guid ApiRequestId, string SubscriptionKey) : IRequest<ApiRequest?>;

public sealed class GetApiRequestByIdQueryHandler : IRequestHandler<GetApiRequestByIdQuery, ApiRequest?>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetApiRequestByIdQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<ApiRequest?> Handle(GetApiRequestByIdQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Requests.AsNoTracking()
            .Include(m => m.Response)
            .FirstOrDefaultAsync(m => m.Id == request.ApiRequestId && m.SubscriptionKey == request.SubscriptionKey, cancellationToken);
    }
}
