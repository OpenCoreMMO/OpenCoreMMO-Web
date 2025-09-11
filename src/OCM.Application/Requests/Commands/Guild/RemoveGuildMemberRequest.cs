using MediatR;
using OCM.Application.Response;

namespace OCM.Application.Requests.Commands.Guild;

public class RemoveGuildMemberRequest : IRequest<OutputResponse>
{
    public int GuildId { get; set; }
    public int PlayerId { get; set; }

    public void SetGuildId(int guildId) => GuildId = guildId;
    public void SetPlayerId(int playerId) => PlayerId = playerId;
}