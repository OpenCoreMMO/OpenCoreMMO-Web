using System.Linq.Expressions;
using MediatR;
using OCM.Application.Requests.Queries;
using OCM.Application.Response;
using OCM.Application.Response.BugReports;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Queries;

public class GetBugReportsQuery(IReportBugRepository reportBugRepository, IPlayerRepository playerRepository)
    : IRequestHandler<GetBugReportsRequest, BasePagedResponseViewModel<IEnumerable<BugReportResponseViewModel>>>
{
    public async Task<BasePagedResponseViewModel<IEnumerable<BugReportResponseViewModel>>> Handle(GetBugReportsRequest request,
        CancellationToken cancellationToken)
    {
        // Ensure all DateTime values are in UTC to avoid PostgreSQL timezone issues
        var createdFromUtc = request.CreatedFrom?.ToUniversalTime();
        var createdToUtc = request.CreatedTo?.ToUniversalTime();
        var closedFromUtc = request.ClosedFrom?.ToUniversalTime();
        var closedToUtc = request.ClosedTo?.ToUniversalTime();

        Expression<Func<ReportBugEntity, bool>> expression = item =>
            (request.PlayerId == null || item.PlayerId == request.PlayerId) &&
            (createdFromUtc == null || item.CreatedAt >= createdFromUtc) &&
            (createdToUtc == null || item.CreatedAt <= createdToUtc) &&
            (closedFromUtc == null || item.ClosedAt >= closedFromUtc) &&
            (closedToUtc == null || item.ClosedAt <= closedToUtc) &&
            (request.Status == null || request.Status == "all" ||
             (request.Status == "opened" && item.ClosedAt == null) ||
             (request.Status == "closed" && item.ClosedAt != null));

        var totalBugReports = await reportBugRepository.CountAllAsync(expression);
        var bugReports = await reportBugRepository.GetPaginatedBugReportsAsync(expression, request.Page, request.Limit);

        // Get player names for the bug reports
        var playerIds = bugReports.Select(b => b.PlayerId).Distinct().ToList();
        var players = new Dictionary<uint, string>();

        foreach (var playerId in playerIds)
        {
            var player = await playerRepository.GetAsync((int)playerId);
            if (player != null)
            {
                players[playerId] = player.Name;
            }
        }

        var response = bugReports.Select(item =>
        {
            var viewModel = (BugReportResponseViewModel)item;
            viewModel.PlayerName = players.ContainsKey(item.PlayerId) ? players[item.PlayerId] : $"Player {item.PlayerId}";
            return viewModel;
        });

        var totalPages = (int)Math.Ceiling((double)totalBugReports / request.Limit);

        return new BasePagedResponseViewModel<IEnumerable<BugReportResponseViewModel>>(response, request.Page,
            request.Limit, totalBugReports, totalPages);
    }
}