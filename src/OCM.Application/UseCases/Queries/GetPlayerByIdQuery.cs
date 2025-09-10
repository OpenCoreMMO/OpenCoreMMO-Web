using MediatR;
using OCM.Application.Requests.Queries;
using OCM.Application.Response.Player;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Queries;

public class GetPlayerByIdQuery(IPlayerRepository playerRepository)
    : IRequestHandler<GetPlayerByIdRequest, PlayerResponseViewModel>
{
    public async Task<PlayerResponseViewModel> Handle(GetPlayerByIdRequest request, CancellationToken cancellationToken)
    {
        return await playerRepository.GetById(request.Id);
    }
}