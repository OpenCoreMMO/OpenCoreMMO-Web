using MediatR;
using OCM.Application.Requests.Queries;
using OCM.Application.Response.Player;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Queries;

public class SearchPlayersQuery(IPlayerRepository playerRepository)
    : IRequestHandler<SearchPlayersRequest, IEnumerable<PlayerResponseViewModel>>
{
    public async Task<IEnumerable<PlayerResponseViewModel>> Handle(SearchPlayersRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.SearchTerm))
            return Enumerable.Empty<PlayerResponseViewModel>();

        var players = await playerRepository.SearchPlayersByNameAsync(request.SearchTerm, request.MaxResults);
        return players.Select(p => (PlayerResponseViewModel)p);
    }
}