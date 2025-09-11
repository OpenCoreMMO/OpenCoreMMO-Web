using MediatR;
using OCM.Application.Requests.Commands;
using OCM.Application.Response;
using OCM.Application.Response.Constants;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Commands.World;

public class DeleteWorldCommand(IWorldRepository worldRepository)
    : IRequestHandler<DeleteWorldCommandRequest, OutputResponse>
{
    public async Task<OutputResponse> Handle(DeleteWorldCommandRequest request, CancellationToken cancellationToken)
    {
        var worldEntity = await worldRepository.GetAsync(request.Id);

        if (worldEntity is null)
            return new OutputResponse(ErrorMessage.WorldNotFound);

        if (worldEntity.DeletedAt is not null)
            return new OutputResponse(ErrorMessage.WorldAlreadyDeleted);

        worldEntity.DeletedAt = DateTime.UtcNow;

        await worldRepository.Update(worldEntity);

        return new OutputResponse();
    }
}