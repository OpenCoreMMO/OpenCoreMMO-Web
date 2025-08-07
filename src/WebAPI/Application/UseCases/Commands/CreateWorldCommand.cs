using MediatR;
using OCM.Application.Requests.Commands;
using OCM.Application.Response;
using OCM.Application.Response.Constants;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Interfaces;

namespace NeoServer.Web.API.Application.UseCases.Commands;

public class CreateWorldCommand(IWorldRepository worldRepository) : IRequestHandler<CreateWorldRequest, OutputResponse>
{
    public async Task<OutputResponse> Handle(CreateWorldRequest request, CancellationToken cancellationToken)
    {
        var worldAlreadyExist = await worldRepository.GetByNameOrIpPort(request.Name, request.Ip, request.Port);

        if (worldAlreadyExist is not null)
            return new OutputResponse(ErrorMessage.WorldAlreadyExist);

        var world = new WorldEntity
        {
            Name = request.Name,
            Ip = request.Ip,
            Port = request.Port,
            Region = request.Region,
            PvpType = request.PvpType,
            AntiCheatEnabled = request.AntiCheatEnabled,
            TransferEnabled = request.TransferEnabled,
            RequiresPremium = request.RequiresPremium,
            WorldType = request.WorldType,
            MaxCapacity = request.MaxCapacity,
            CreatedAt = DateTime.UtcNow
        };

        await worldRepository.Insert(world);

        return new OutputResponse(world.Id);
    }
}