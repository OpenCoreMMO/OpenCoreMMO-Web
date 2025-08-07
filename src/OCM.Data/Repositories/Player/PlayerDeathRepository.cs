using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OCM.Infrastructure.Contexts;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Interfaces;
using Serilog;

namespace OCM.Infrastructure.Repositories.Player;

public class PlayerDeathRepository(DbContextOptions<NeoContext> contextOptions, ILogger logger)
    : BaseRepository<PlayerDeathEntity>(contextOptions,
        logger), IPlayerDeathRepository
{
    public IEnumerable<PlayerDeathEntity> GetPlayerKills(int playerId)
    {
        using var context = NewDbContext;

        return context.PlayerDeathKillers
            .Include(x => x.PlayerDeath)
            .Where(x => x.PlayerId == playerId)
            .Select(x => x.PlayerDeath);
    }
}