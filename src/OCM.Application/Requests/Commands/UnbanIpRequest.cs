using MediatR;
using OCM.Application.Response;

namespace OCM.Application.Requests.Commands;

public class UnbanIpRequest : IRequest<OutputResponse>, ICommandBase
{
    public int IpBanId { get; private set; }

    public void SetIpBanId(int ipBanId)
    {
        IpBanId = ipBanId;
    }
}