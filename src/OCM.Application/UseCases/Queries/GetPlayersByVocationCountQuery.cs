using MediatR;
using OCM.Application.Requests.Queries;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Queries;

public class GetPlayersByVocationCountQuery(IPlayerRepository playerRepository)
    : IRequestHandler<GetPlayersByVocationCountRequest, Dictionary<int, int>>
{
    public async Task<Dictionary<int, int>> Handle(GetPlayersByVocationCountRequest request, CancellationToken cancellationToken)
    {
        var vocationCounts = new Dictionary<int, int>();

        // Base vocations (1-4)
        for (int vocationId = 1; vocationId <= 4; vocationId++)
        {
            var count = await playerRepository.CountAllAsync(p => p.Vocation == vocationId);
            vocationCounts[vocationId] = count;
        }

        return vocationCounts;
    }
}