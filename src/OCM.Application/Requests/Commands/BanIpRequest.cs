using MediatR;
using OCM.Application.Response;

namespace OCM.Application.Requests.Commands;

public class BanIpRequest : IRequest<OutputResponse>, ICommandBase
{
    public string Ip { get; set; }

    public string Reason { get; set; }

    public int Days { get; set; }
}