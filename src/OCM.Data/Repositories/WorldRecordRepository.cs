using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OCM.Infrastructure.Contexts;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Interfaces;
using Serilog;

namespace OCM.Infrastructure.Repositories;

public class WorldRecordRepository : BaseRepository<WorldRecordEntity>, IWorldRecordRepository
{
    #region constructors

    public WorldRecordRepository(DbContextOptions<NeoContext> contextOptions, ILogger logger) : base(contextOptions,
        logger)
    {
    }

    #endregion

    public async Task<WorldRecordEntity> GetLastFromWord(int worldId)
    {
        await using var context = NewDbContext;
        return await context.WorldRecords
            .OrderBy(c => c.CreatedAt)
            .LastOrDefaultAsync(c => c.WorldId == worldId);
    }
}