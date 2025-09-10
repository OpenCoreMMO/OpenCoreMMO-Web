using System.Linq.Expressions;
using MediatR;
using OCM.Application.Requests.Queries;
using OCM.Application.Response;
using OCM.Application.Response.World;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Queries;

public class GetWorldsQuery(IWorldRepository worldRepository)
    : IRequestHandler<GetWorldsRequest, BasePagedResponseViewModel<IEnumerable<WorldResponseViewModel>>>
{
    public async Task<BasePagedResponseViewModel<IEnumerable<WorldResponseViewModel>>> Handle(GetWorldsRequest request,
        CancellationToken cancellationToken)
    {
        Expression<Func<WorldEntity, bool>> expression = item =>
            (request.Name == null || item.Name.ToLower().Contains(request.Name.ToLower())) &&
            (request.Continent == null || item.Region == request.Continent) &&
            (request.PvpType == null || item.PvpType == request.PvpType) &&
            (request.Type == null || item.WorldType == request.Type) &&
            (request.TransferEnabled == null || item.TransferEnabled == request.TransferEnabled) &&
            (request.AntiCheatEnabled == null || item.AntiCheatEnabled == request.AntiCheatEnabled) &&
            (request.RequiresPremium == null || item.RequiresPremium == request.RequiresPremium);

        var totalWorlds = await worldRepository.CountAllAsync(expression);
        var players = await worldRepository.GetPaginatedWorldsAsync(expression, request.Page, request.Limit);
        var response = players.Select(item => (WorldResponseViewModel)item);

        var totalPages = (int)Math.Ceiling((double)totalWorlds / request.Limit);

        return new BasePagedResponseViewModel<IEnumerable<WorldResponseViewModel>>(response, request.Page,
            request.Limit, totalWorlds, totalPages);
    }
}