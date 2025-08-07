using System.Collections.Generic;
using System.Threading.Tasks;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Interfaces;

public interface IGuildRepository : IBaseRepositoryNeo<GuildEntity>
{
    Task<IEnumerable<GuildEntity>> GetAll();
    Task<GuildEntity> GetByName(string name);
    Task<GuildEntity> GetById(int id);
}