using System.Linq.Expressions;
using MediatR;
using OCM.Application.Requests.Queries;
using OCM.Application.Response;
using OCM.Application.Response.Guild;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Queries;

public class GetGuildsQuery(IGuildRepository guildRepository)
    : IRequestHandler<GetGuildsRequest, BasePagedResponseViewModel<IEnumerable<GuildResponseViewModel>>>
{
    public async Task<BasePagedResponseViewModel<IEnumerable<GuildResponseViewModel>>> Handle(GetGuildsRequest request,
        CancellationToken cancellationToken)
    {
        Expression<Func<GuildEntity, bool>> expression = item =>
            (request.Name == null || item.Name.ToLower().Contains(request.Name.ToLower()));

        var totalGuilds = await guildRepository.CountAllAsync(expression);
        var guilds = await guildRepository.GetPaginatedGuildsAsync(expression, request.Page, request.Limit);
        var response = guilds.Select(item => (GuildResponseViewModel)item);

        var totalPages = (int)Math.Ceiling((double)totalGuilds / request.Limit);

        return new BasePagedResponseViewModel<IEnumerable<GuildResponseViewModel>>(response, request.Page,
            request.Limit, totalGuilds, totalPages);
    }
}