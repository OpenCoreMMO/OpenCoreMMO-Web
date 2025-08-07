using Microsoft.EntityFrameworkCore;
using OCM.Infrastructure.Contexts;
using OCM.Infrastructure.Factory;

namespace OCM.Infrastructure.Providers.SQLite;

public static class DbContextFactoryExtensions
{
    public static DbContextOptions<NeoContext> UseSQLite(this DbContextFactory factory, string name)
    {
        var builder = new DbContextOptionsBuilder<NeoContext>();
        builder.UseSqlite(name);

        return builder.Options;
    }
}