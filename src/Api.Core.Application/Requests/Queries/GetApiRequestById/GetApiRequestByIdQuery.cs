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

    public Task<ApiRequest?> Handle(GetApiRequestByIdQuery request, CancellationToken cancellationToken)
    {
        // Hay un error o bug que afecta el rendimiento de la consulta se hace utilizando los metodos asincronos
        // Leer: https://github.com/dotnet/efcore/issues/18221 y https://github.com/dotnet/SqlClient/issues/245 y https://github.com/dotnet/SqlClient/issues/593
        // Todo: Convertir a async cuando EF Core libere una una version que corrija el rendimiento
        return Task.FromResult(_applicationDbContext.Requests.AsNoTracking()
            .Include(m => m.Response)
            .FirstOrDefault(m => m.Id == request.ApiRequestId && m.SubscriptionKey == request.SubscriptionKey));
    }
}