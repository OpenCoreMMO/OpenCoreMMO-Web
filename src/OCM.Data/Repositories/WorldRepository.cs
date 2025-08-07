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

public class WorldRepository : BaseRepository<WorldEntity>, IWorldRepository
{
    public WorldRepository(DbContextOptions<NeoContext> contextOptions, ILogger logger) : base(contextOptions,
        logger)
    {
    }

    public async Task<IEnumerable<WorldEntity>> GetPaginatedWorldsAsync(Expression<Func<WorldEntity, bool>> filter,
        int page, int limit)
    {
        await using var neoContext = NewDbContext;
        var skip = (page - 1) * limit;
        return await neoContext.Worlds.Where(filter).Skip(skip).Take(limit).ToListAsync();
    }

    public async Task<WorldEntity> GetByNameOrIpPort(string name, string ip, int port)
    {
        await using var context = NewDbContext;

        return await context.Worlds
            .Where(x => x.Name.ToLower().Equals(name.ToLower()) || (x.Ip.Equals(ip) && x.Port == port))
            .SingleOrDefaultAsync();
    }
}