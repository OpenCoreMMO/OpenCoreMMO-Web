using System.Collections.Generic;
using System.Threading.Tasks;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Interfaces;

public interface IPlayerDepotItemRepository : IBaseRepositoryNeo<PlayerDepotItemEntity>
{
    Task<IEnumerable<PlayerDepotItemEntity>> GetByPlayerId(uint id);
    //  Task Save(IPlayer player, Depot depot);
}