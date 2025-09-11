using MediatR;
using OCM.Application.Requests.Commands.Guild;
using OCM.Application.Response;
using OCM.Application.Response.Constants;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Commands.Guild;

public class RemoveGuildMemberCommand(IGuildRepository guildRepository)
    : IRequestHandler<RemoveGuildMemberRequest, OutputResponse>
{
    public async Task<OutputResponse> Handle(RemoveGuildMemberRequest request, CancellationToken cancellationToken)
    {
        var membership = await guildRepository.GetMembershipAsync(request.GuildId, request.PlayerId);

        if (membership is null)
            return new OutputResponse("Guild membership not found");

        await guildRepository.RemoveMembership(membership);

        return new OutputResponse();
    }
}