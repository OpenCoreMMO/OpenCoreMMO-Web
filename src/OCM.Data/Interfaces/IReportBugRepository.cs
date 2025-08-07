using System.Threading.Tasks;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Interfaces;

public interface IReportBugRepository : IBaseRepositoryNeo<ReportBugEntity>
{
    Task<ReportBugEntity> GetLatestReportPendingByPlayerIdAsync(uint playerId);
}