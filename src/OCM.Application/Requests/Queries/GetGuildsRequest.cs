using MediatR;
using OCM.Application.Response;
using OCM.Application.Response.Guild;

namespace OCM.Application.Requests.Queries;

public class GetGuildsRequest : IRequest<BasePagedResponseViewModel<IEnumerable<GuildResponseViewModel>>>
{
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 10;
    public string Name { get; set; }
}