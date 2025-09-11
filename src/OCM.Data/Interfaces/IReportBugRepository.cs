using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Interfaces;

public interface IReportBugRepository : IBaseRepositoryNeo<ReportBugEntity>
{
    Task<ReportBugEntity> GetLatestReportPendingByPlayerIdAsync(uint playerId);
    Task<IEnumerable<ReportBugEntity>> GetPaginatedBugReportsAsync(Expression<Func<ReportBugEntity, bool>> filter, int page, int limit);
}