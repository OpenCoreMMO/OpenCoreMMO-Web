using MediatR;
using OCM.Application.Requests.Queries;
using OCM.Application.Response.Guild;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Queries;

public class GetGuildByIdQuery(IGuildRepository guildRepository)
    : IRequestHandler<GetGuildByIdRequest, GuildResponseViewModel>
{
    public async Task<GuildResponseViewModel> Handle(GetGuildByIdRequest request, CancellationToken cancellationToken)
    {
        var guild = await guildRepository.GetById(request.Id);
        return (GuildResponseViewModel)guild;
    }
}