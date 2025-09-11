using MediatR;
using OCM.Application.Response.Guild;

namespace OCM.Application.Requests.Queries;

public class GetGuildByIdRequest : IRequest<GuildResponseViewModel>
{
    public int Id { get; set; }
}