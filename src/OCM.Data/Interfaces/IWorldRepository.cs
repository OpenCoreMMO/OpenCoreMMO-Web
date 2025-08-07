using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Interfaces;

public interface IWorldRepository : IBaseRepositoryNeo<WorldEntity>
{
    Task<IEnumerable<WorldEntity>> GetPaginatedWorldsAsync(Expression<Func<WorldEntity, bool>> filter, int page,
        int limit);

    Task<WorldEntity> GetByNameOrIpPort(string name, string ip, int port);
}