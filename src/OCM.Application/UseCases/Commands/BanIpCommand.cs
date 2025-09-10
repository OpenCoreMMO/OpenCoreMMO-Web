using MediatR;
using OCM.Application.Requests.Commands;
using OCM.Application.Response;
using OCM.Application.Response.Constants;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Commands;

public class BanIpCommand(IIpBansRepository ipBansRepository) : IRequestHandler<BanIpRequest, OutputResponse>
{
    public async Task<OutputResponse> Handle(BanIpRequest request, CancellationToken cancellationToken)
    {
        var ipBan = await ipBansRepository.ExistBan(request.Ip);

        if (ipBan is not null)
            return new OutputResponse(ErrorMessage.IpBanished);

        var entity = new IpBanEntity
        {
            Ip = request.Ip,
            BannedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(request.Days),
            Reason = request.Reason,
            BannedBy = 1 //  TODO: Get the user id from the request with the token
        };

        await ipBansRepository.Insert(entity);
        return new OutputResponse();
    }
}