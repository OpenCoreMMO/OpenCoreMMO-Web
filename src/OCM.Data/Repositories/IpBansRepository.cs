using System;
using System.Linq;
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
            .Where(x => x.Ip.Equals(Ip) && x.ExpiresAt.Date >= DateTime.UtcNow.Date)
            .SingleOrDefaultAsync();
    }
}