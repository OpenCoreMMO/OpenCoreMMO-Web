using MediatR;
using OCM.Application.Response.World;

namespace OCM.Application.Requests.Queries;

public class GetWorldByIdRequest : IRequest<WorldResponseViewModel>
{
    public int Id { get; set; }
}