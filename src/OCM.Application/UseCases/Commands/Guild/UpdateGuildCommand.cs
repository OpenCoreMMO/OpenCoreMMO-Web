using MediatR;
using OCM.Application.Requests.Commands.Guild;
using OCM.Application.Response;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Commands.Guild;

public class UpdateGuildCommand(IGuildRepository guildRepository)
    : IRequestHandler<UpdateGuildRequest, OutputResponse>
{
    public async Task<OutputResponse> Handle(UpdateGuildRequest request, CancellationToken cancellationToken)
    {
        var guildEntity = await guildRepository.GetAsync(request.Id);

        if (guildEntity is null)
            return new OutputResponse("Guild not found");

        // Check for duplicate name (excluding current guild)
        var existingGuild = await guildRepository.GetByName(request.Name);
        if (existingGuild is not null && existingGuild.Id != request.Id)
            return new OutputResponse("Guild with this name already exists");

        guildEntity.Name = request.Name;
        guildEntity.Description = request.Description;
        guildEntity.Level = request.Level;
        guildEntity.Points = request.Points;
        guildEntity.BankAmount = request.BankAmount;

        await guildRepository.Update(guildEntity);

        return new OutputResponse();
    }
}