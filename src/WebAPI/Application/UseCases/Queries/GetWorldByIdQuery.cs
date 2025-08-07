using MediatR;
using OCM.Application.Requests.Queries;
using OCM.Application.Response.World;
using OCM.Infrastructure.Interfaces;

namespace NeoServer.Web.API.Application.UseCases.Queries;

public class GetWorldByIdQuery(IWorldRepository worldRepository)
    : IRequestHandler<GetWorldByIdRequest, WorldResponseViewModel>
{
    public async Task<WorldResponseViewModel> Handle(GetWorldByIdRequest request, CancellationToken cancellationToken)
    {
        return await worldRepository.GetAsync(request.Id);
    }
}