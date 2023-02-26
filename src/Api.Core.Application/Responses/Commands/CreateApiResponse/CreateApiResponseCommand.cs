using Api.Core.Application.Common.Interfaces;
using Api.Core.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Core.Application.Responses.Commands.CreateApiResponse;

public sealed record CreateApiResponseCommand(ApiResponseBase ApiResponse) : IRequest;

public sealed class CreateApiResponseCommandHandler : IRequestHandler<CreateApiResponseCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public CreateApiResponseCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task Handle(CreateApiResponseCommand request, CancellationToken cancellationToken)
    {
        ApiRequestBase apiRequest = await _applicationDbContext.Requests.FirstAsync(c => c.Id == request.ApiResponse.Id, cancellationToken);

        apiRequest.Response = request.ApiResponse;
        apiRequest.Status = RequestStatus.Processed;

        await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }
}
