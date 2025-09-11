using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using OCM.Infrastructure.Contexts;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Interfaces;
using Serilog;

namespace OCM.Infrastructure.Repositories.Player;

public class PlayerRepository : BaseRepository<PlayerEntity>, IPlayerRepository
{
    #region constructors

    public PlayerRepository(DbContextOptions<NeoContext> contextOptions, ILogger logger) : base(contextOptions,
        logger)
    {
    }

    #endregion

    public async Task UpdateAllPlayersToOffline()
    {
        const string sql = "UPDATE Player SET Online = 0";

        await using var context = NewDbContext;

        if (!context.Database.IsRelational()) return;

        await using var connection = context.Database.GetDbConnection();

        await connection.ExecuteAsync(sql);
    }

    public async Task Add(PlayerEntity player)
    {
        await using var context = NewDbContext;
        await context.AddAsync(player);
        await context.SaveChangesAsync();
    }

    public async Task<List<PlayerOutfitAddonEntity>> GetOutfitAddons(int playerId)
    {
        await using var context = NewDbContext;
        return await context.PlayerOutfitAddons.Where(x => x.PlayerId == playerId).ToListAsync();
    }

    public async Task<PlayerEntity> GetByName(string playerName)
    {
        await using var context = NewDbContext;
        //todo: find a way to use invariant culture. it currently doesn't work with sqlite
        return await context.Players.FirstOrDefaultAsync(x => x.Name.ToLower() == playerName.ToLower());
    }

    public async Task<PlayerEntity> GetById(int id)
    {
        await using var context = NewDbContext;
        return await context.Players
            .Include(p => p.World)
            .Include(p => p.Town)
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task UpdatePlayerOnlineStatus(uint playerId, bool status)
    {
        await using var context = NewDbContext;

        var player = await context.Players.SingleOrDefaultAsync(x => x.Id == playerId);
        if (player is null) return;

        player.Online = status;

        await context.SaveChangesAsync();
    }


    public async Task<IEnumerable<PlayerEntity>> GetPaginatedPlayersAsync(Expression<Func<PlayerEntity, bool>> filter,
        int page, int limit)
    {
        await using var neoContext = NewDbContext;
        var skip = (page - 1) * limit;
        return await neoContext.Players.Where(filter).Skip(skip).Take(limit).ToListAsync();
    }

    public async Task<IEnumerable<PlayerEntity>> SearchPlayersByNameAsync(string searchTerm, int maxResults)
    {
        await using var context = NewDbContext;
        return await context.Players
            .Where(p => p.Name.ToLower().StartsWith(searchTerm.ToLower()))
            .OrderBy(p => p.Name)
            .Take(maxResults)
            .ToListAsync();
    }
}