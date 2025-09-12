using System.Linq.Expressions;
using MediatR;
using OCM.Application.Requests.Queries;
using OCM.Application.Response;
using OCM.Application.Response.IpBans;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Queries;

public class GetIpBansQuery(IIpBansRepository ipBansRepository, IPlayerRepository playerRepository)
    : IRequestHandler<GetIpBansRequest, BasePagedResponseViewModel<IEnumerable<IpBanResponseViewModel>>>
{
    public async Task<BasePagedResponseViewModel<IEnumerable<IpBanResponseViewModel>>> Handle(GetIpBansRequest request,
        CancellationToken cancellationToken)
    {
        // Ensure all DateTime values are in UTC to avoid PostgreSQL timezone issues
        var createdFromUtc = request.CreatedFrom?.ToUniversalTime();
        var createdToUtc = request.CreatedTo?.ToUniversalTime();
        var expiresFromUtc = request.ExpiresFrom?.ToUniversalTime();
        var expiresToUtc = request.ExpiresTo?.ToUniversalTime();

        Expression<Func<IpBanEntity, bool>> expression = item =>
            (request.Status != "deleted" ? item.DeletedAt == null : item.DeletedAt != null) &&
            (string.IsNullOrWhiteSpace(request.IpAddress) || item.Ip.Contains(request.IpAddress)) &&
            (request.BannedById == null || item.BannedBy == request.BannedById) &&
            (createdFromUtc == null || item.BannedAt >= createdFromUtc) &&
            (createdToUtc == null || item.BannedAt <= createdToUtc) &&
            (expiresFromUtc == null || item.ExpiresAt >= expiresFromUtc) &&
            (expiresToUtc == null || item.ExpiresAt <= expiresToUtc) &&
            (request.Status == null || request.Status == "all" ||
              (request.Status == "active" && item.ExpiresAt > DateTime.UtcNow) ||
              (request.Status == "expired" && item.ExpiresAt <= DateTime.UtcNow) ||
              (request.Status == "deleted"));

        var totalIpBans = await ipBansRepository.CountAllAsync(expression);
        var ipBans = await ipBansRepository.GetPaginatedIpBansAsync(expression, request.Page, request.Limit);

        // Get player names for the IP bans
        var playerIds = ipBans.Select(b => (uint)b.BannedBy).Distinct().ToList();
        var players = new Dictionary<uint, string>();

        foreach (var playerId in playerIds)
        {
            var player = await playerRepository.GetAsync((int)playerId);
            if (player != null)
            {
                players[playerId] = player.Name;
            }
        }

        var response = ipBans.Select(item =>
        {
            var viewModel = (IpBanResponseViewModel)item;
            viewModel.BannedByName = players.ContainsKey((uint)item.BannedBy) ? players[(uint)item.BannedBy] : $"Player {item.BannedBy}";
            return viewModel;
        });

        var totalPages = (int)Math.Ceiling((double)totalIpBans / request.Limit);

        return new BasePagedResponseViewModel<IEnumerable<IpBanResponseViewModel>>(response, request.Page,
            request.Limit, totalIpBans, totalPages);
    }
}