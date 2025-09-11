using MediatR;
using OCM.Application.Requests.Commands;
using OCM.Application.Response;
using OCM.Application.Response.Constants;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Commands.World;

public class UpdateWorldCommand(IWorldRepository worldRepository)
    : IRequestHandler<UpdateWorldRequest, OutputResponse>
{
    public async Task<OutputResponse> Handle(UpdateWorldRequest request, CancellationToken cancellationToken)
    {
        var worldEntity = await worldRepository.GetAsync(request.Id);

        if (worldEntity is null)
            return new OutputResponse(ErrorMessage.WorldNotFound);

        if (worldEntity.DeletedAt is not null)
            return new OutputResponse(ErrorMessage.WorldAlreadyDeleted);

        // Check for duplicate name/IP/port (excluding current world)
        var existingWorld = await worldRepository.GetByNameOrIpPort(request.Name, request.Ip, request.Port);
        if (existingWorld is not null && existingWorld.Id != request.Id)
            return new OutputResponse(ErrorMessage.WorldAlreadyExist);

        worldEntity.Name = request.Name;
        worldEntity.Ip = request.Ip;
        worldEntity.Port = request.Port;
        worldEntity.Region = request.Region;
        worldEntity.PvpType = request.PvpType;
        worldEntity.WorldType = request.WorldType;
        worldEntity.RequiresPremium = request.RequiresPremium;
        worldEntity.TransferEnabled = request.TransferEnabled;
        worldEntity.AntiCheatEnabled = request.AntiCheatEnabled;
        worldEntity.MaxCapacity = request.MaxCapacity;

        await worldRepository.Update(worldEntity);

        return new OutputResponse();
    }
}