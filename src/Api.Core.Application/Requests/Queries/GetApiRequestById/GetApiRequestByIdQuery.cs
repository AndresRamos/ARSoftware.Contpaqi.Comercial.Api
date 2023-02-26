using Api.Core.Application.Common.Interfaces;
using Api.Core.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Core.Application.Requests.Queries.GetApiRequestById;

public sealed record GetApiRequestByIdQuery(Guid ApiRequestId) : IRequest<ApiRequestBase?>;

public sealed class GetApiRequestByIdQueryHandler : IRequestHandler<GetApiRequestByIdQuery, ApiRequestBase?>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetApiRequestByIdQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<ApiRequestBase?> Handle(GetApiRequestByIdQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Requests.Include(m => m.Response)
            .FirstOrDefaultAsync(m => m.Id == request.ApiRequestId, cancellationToken);
    }
}
