using System.Linq.Expressions;
using MediatR;
using OCM.Application.Requests.Queries;
using OCM.Application.Response;
using OCM.Application.Response.Town;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Queries;

public class GetTownsQuery(ITownRepository townRepository)
    : IRequestHandler<GetTownsRequest, BasePagedResponseViewModel<IEnumerable<TownResponseViewModel>>>
{
    public async Task<BasePagedResponseViewModel<IEnumerable<TownResponseViewModel>>> Handle(GetTownsRequest request,
        CancellationToken cancellationToken)
    {
        Expression<Func<TownEntity, bool>> expression = item =>
            (request.Name == null || item.Name.ToLower().Contains(request.Name.ToLower())) &&
            (request.WorldId == null || item.WorldId == request.WorldId);

        var totalTowns = await townRepository.CountAllAsync(expression);
        var towns = await townRepository.GetPaginatedTownsAsync(expression, request.Page, request.Limit);
        var response = towns.Select(item => (TownResponseViewModel)item);

        var totalPages = (int)Math.Ceiling((double)totalTowns / request.Limit);

        return new BasePagedResponseViewModel<IEnumerable<TownResponseViewModel>>(response, request.Page,
            request.Limit, totalTowns, totalPages);
    }
}