using MediatR;
using OCM.Application.Requests.Queries;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Queries;

public class GetTotalPlayersCountQuery(IPlayerRepository playerRepository)
    : IRequestHandler<GetTotalPlayersCountRequest, int>
{
    public async Task<int> Handle(GetTotalPlayersCountRequest request, CancellationToken cancellationToken)
    {
        return await playerRepository.CountAllAsync(_ => true);
    }
}