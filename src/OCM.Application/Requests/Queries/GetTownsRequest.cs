using MediatR;
using OCM.Application.Response;
using OCM.Application.Response.Town;

namespace OCM.Application.Requests.Queries;

public class GetTownsRequest : IRequest<BasePagedResponseViewModel<IEnumerable<TownResponseViewModel>>>
{
    public string Name { get; set; }
    public int? WorldId { get; set; }
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 5;
}