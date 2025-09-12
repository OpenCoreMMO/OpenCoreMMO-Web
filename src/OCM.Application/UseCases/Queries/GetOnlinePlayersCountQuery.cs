using MediatR;
using OCM.Application.Requests.Queries;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Queries;

public class GetOnlinePlayersCountQuery(IPlayerRepository playerRepository)
    : IRequestHandler<GetOnlinePlayersCountRequest, int>
{
    public async Task<int> Handle(GetOnlinePlayersCountRequest request, CancellationToken cancellationToken)
    {
        return await playerRepository.CountAllAsync(p => p.Online);
    }
}