using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OCM.Infrastructure.Contexts;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Interfaces;
using Serilog;

namespace OCM.Infrastructure.Repositories;

public class RoleRepository : BaseRepository<RoleEntity>, IRoleRepository
{
    public RoleRepository(DbContextOptions<NeoContext> contextOptions, ILogger logger) : base(contextOptions, logger)
    {
    }

    public async Task<RoleEntity> GetByNameAsync(string name)
    {
        await using var context = NewDbContext;
        return await context.Roles
            .FirstOrDefaultAsync(r => r.Name == name);
    }
}