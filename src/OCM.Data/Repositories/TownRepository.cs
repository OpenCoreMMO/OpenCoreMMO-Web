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

public class TownRepository : BaseRepository<TownEntity>, ITownRepository
{
    public TownRepository(DbContextOptions<NeoContext> contextOptions, ILogger logger) : base(contextOptions,
        logger)
    {
    }

    public async Task<IEnumerable<TownEntity>> GetPaginatedTownsAsync(Expression<Func<TownEntity, bool>> filter,
        int page, int limit)
    {
        await using var neoContext = NewDbContext;
        var skip = (page - 1) * limit;
        return await neoContext.Towns.Where(filter).Skip(skip).Take(limit).ToListAsync();
    }
}