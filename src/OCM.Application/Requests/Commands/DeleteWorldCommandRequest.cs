using MediatR;
using OCM.Application.Response;

namespace OCM.Application.Requests.Commands;

public class DeleteWorldCommandRequest : IRequest<OutputResponse>, ICommandBase
{
    public int Id { get; set; }
}