using MediatR;

namespace OCM.Application.Requests.Queries;

public class GetPlayersByVocationCountRequest : IRequest<Dictionary<int, int>>
{
}