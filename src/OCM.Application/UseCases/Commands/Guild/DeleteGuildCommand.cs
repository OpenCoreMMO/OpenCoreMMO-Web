using MediatR;
using OCM.Application.Requests.Commands;
using OCM.Application.Requests.Commands.Guild;
using OCM.Application.Response;
using OCM.Application.Response.Constants;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Commands.Guild;

public class DeleteGuildCommand(IGuildRepository guildRepository)
    : IRequestHandler<DeleteGuildRequest, OutputResponse>
{
    public async Task<OutputResponse> Handle(DeleteGuildRequest request, CancellationToken cancellationToken)
    {
        var guildEntity = await guildRepository.GetAsync(request.Id);

        if (guildEntity is null)
            return new OutputResponse("Guild not found");

        await guildRepository.Delete(guildEntity);

        return new OutputResponse();
    }
}