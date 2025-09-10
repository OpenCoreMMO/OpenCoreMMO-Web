using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Interfaces;

public interface ITownRepository : IBaseRepositoryNeo<TownEntity>
{
    Task<IEnumerable<TownEntity>> GetPaginatedTownsAsync(Expression<Func<TownEntity, bool>> filter, int page,
        int limit);
}