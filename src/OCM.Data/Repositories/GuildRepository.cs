using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OCM.Infrastructure.Contexts;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Interfaces;
using Serilog;

namespace OCM.Infrastructure.Repositories;

public class GuildRepository : BaseRepository<GuildEntity>, IGuildRepository
{
    #region constructors

    public GuildRepository(DbContextOptions<NeoContext> contextOptions, ILogger logger) : base(contextOptions,
        logger)
    {
    }

    #endregion

    public async Task<IEnumerable<GuildEntity>> GetAll()
    {
        await using var context = NewDbContext;
        return await context.Guilds.Include(x => x.Members).ThenInclude(x => x.Rank).ToListAsync();
    }

    public async Task<GuildEntity> GetByName(string name)
    {
        await using var context = NewDbContext;
        return await context.Guilds.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<GuildEntity> GetById(int id)
    {
        await using var context = NewDbContext;
        return await context.Guilds
            .Include(x => x.Owner)
            .Include(x => x.Members)
            .ThenInclude(x => x.Rank)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<GuildEntity>> GetPaginatedGuildsAsync(System.Linq.Expressions.Expression<System.Func<GuildEntity, bool>> filter, int page, int limit)
    {
        await using var context = NewDbContext;
        var skip = (page - 1) * limit;
        return await context.Guilds
            .Where(filter)
            .Include(x => x.Owner)
            .Include(x => x.Members)
            .Skip(skip)
            .Take(limit)
            .ToListAsync();
    }
}