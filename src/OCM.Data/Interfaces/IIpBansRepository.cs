using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Interfaces;

public interface IIpBansRepository : IBaseRepositoryNeo<IpBanEntity>
{
    Task<IpBanEntity> ExistBan(string Ip);
    Task<IEnumerable<IpBanEntity>> GetPaginatedIpBansAsync(Expression<Func<IpBanEntity, bool>> filter, int page, int limit);
}