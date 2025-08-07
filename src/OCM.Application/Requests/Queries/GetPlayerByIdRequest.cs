using MediatR;
using OCM.Application.Response.Player;

namespace OCM.Application.Requests.Queries;

public class GetPlayerByIdRequest : IRequest<PlayerResponseViewModel>
{
    public int Id { get; set; }
}