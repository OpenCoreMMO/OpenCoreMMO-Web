using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Interfaces;

public interface IPlayerRepository : IBaseRepositoryNeo<PlayerEntity>
{
    Task UpdateAllPlayersToOffline();
    Task Add(PlayerEntity player);
    Task<List<PlayerOutfitAddonEntity>> GetOutfitAddons(int playerId);
    Task UpdatePlayerOnlineStatus(uint playerId, bool status);
    Task<PlayerEntity> GetByName(string playerName);

    /// <summary>
    ///     Save player info, inventory, backpack and depot
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    Task<IEnumerable<PlayerEntity>> GetPaginatedPlayersAsync(Expression<Func<PlayerEntity, bool>> filter, int page,
        int limit);

    Task<PlayerEntity> GetById(int id);
}