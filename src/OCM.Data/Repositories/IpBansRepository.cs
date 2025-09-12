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

public class IpBansRepository : BaseRepository<IpBanEntity>, IIpBansRepository
{
    public IpBansRepository(DbContextOptions<NeoContext> contextOptions, ILogger logger) : base(contextOptions,
        logger)
    {
    }

    public async Task<IpBanEntity> ExistBan(string Ip)
    {
        await using var context = NewDbContext;

        return await context.IpBans
            .Where(x => x.DeletedAt == null && x.Ip.Equals(Ip) && x.ExpiresAt > DateTime.UtcNow)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<IpBanEntity>> GetPaginatedIpBansAsync(Expression<Func<IpBanEntity, bool>> filter, int page, int limit)
    {
        await using var context = NewDbContext;
        return await context.IpBans
            .Where(filter)
            .OrderByDescending(x => x.BannedAt)
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();
    }
}