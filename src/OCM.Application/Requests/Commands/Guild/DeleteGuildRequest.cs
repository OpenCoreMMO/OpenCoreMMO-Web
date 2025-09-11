using MediatR;
using OCM.Application.Response;

namespace OCM.Application.Requests.Commands.Guild;

public class DeleteGuildRequest : IRequest<OutputResponse>
{
    public int Id { get; set; }
}