using System.Collections.Generic;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Interfaces;

public interface IPlayerDeathRepository : IBaseRepositoryNeo<PlayerDeathEntity>
{
    IEnumerable<PlayerDeathEntity> GetPlayerKills(int playerId);
    //new void Save(IPlayer deadPlayer, DamageRecordResult damageRecordResult);
}