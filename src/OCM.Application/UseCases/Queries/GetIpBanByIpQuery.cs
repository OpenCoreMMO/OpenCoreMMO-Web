using MediatR;
using OCM.Application.Requests.Queries;
using OCM.Application.Response.IpBans;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Queries;

public class GetIpBanByIpQuery(IIpBansRepository ipBansRepository)
    : IRequestHandler<GetIpBanByIpRequest, IpBanResponseViewModel>
{
    public async Task<IpBanResponseViewModel> Handle(GetIpBanByIpRequest request, CancellationToken cancellationToken)
    {
        return await ipBansRepository.ExistBan(request.Ip);
    }
}