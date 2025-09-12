using MediatR;
using OCM.Application.Requests.Commands;
using OCM.Application.Response;
using OCM.Application.Response.Constants;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Commands;

public class UnbanIpCommand(IIpBansRepository ipBansRepository) : IRequestHandler<UnbanIpRequest, OutputResponse>
{
    public async Task<OutputResponse> Handle(UnbanIpRequest request, CancellationToken cancellationToken)
    {
        var ipBan = await ipBansRepository.GetAsync(request.IpBanId);

        if (ipBan == null)
            return new OutputResponse("IP ban not found.");

        if (ipBan.DeletedAt != null)
            return new OutputResponse("IP ban already deleted.");

        ipBan.DeletedAt = DateTime.UtcNow;
        ipBan.DeletedBy = 1; // TODO: Get the user id from the request with the token
        await ipBansRepository.Update(ipBan);

        return new OutputResponse();
    }
}