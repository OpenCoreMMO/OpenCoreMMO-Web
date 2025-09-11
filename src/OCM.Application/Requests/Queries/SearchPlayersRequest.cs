using MediatR;
using OCM.Application.Response.Player;

namespace OCM.Application.Requests.Queries;

public class SearchPlayersRequest : IRequest<IEnumerable<PlayerResponseViewModel>>
{
    public string SearchTerm { get; set; }
    public int MaxResults { get; set; } = 10;
}