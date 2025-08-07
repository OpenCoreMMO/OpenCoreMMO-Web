using System.Threading.Tasks;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Interfaces;

public interface IWorldRecordRepository : IBaseRepositoryNeo<WorldRecordEntity>
{
    Task<WorldRecordEntity> GetLastFromWord(int worldId);
}