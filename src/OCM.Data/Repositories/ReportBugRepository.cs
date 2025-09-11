using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OCM.Infrastructure.Contexts;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Interfaces;
using Serilog;

namespace OCM.Infrastructure.Repositories;

public class ReportBugRepository : BaseRepository<ReportBugEntity>, IReportBugRepository
{
    public ReportBugRepository(DbContextOptions<NeoContext> contextOptions, ILogger logger) : base(contextOptions,
        logger)
    {
    }

    public async Task<ReportBugEntity> GetLatestReportPendingByPlayerIdAsync(uint playerId)
    {
        await using var context = NewDbContext;
        return await context.ReportBugs
            .Where(x => x.PlayerId == playerId && x.ClosedAt == null)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ReportBugEntity>> GetPaginatedBugReportsAsync(Expression<Func<ReportBugEntity, bool>> filter, int page, int limit)
    {
        await using var context = NewDbContext;
        return await context.ReportBugs
            .Where(filter)
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();
    }
}