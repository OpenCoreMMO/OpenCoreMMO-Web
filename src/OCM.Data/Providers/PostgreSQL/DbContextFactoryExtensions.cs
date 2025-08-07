using Microsoft.EntityFrameworkCore;
using OCM.Infrastructure.Contexts;
using OCM.Infrastructure.Factory;

namespace OCM.Infrastructure.Providers.PostgreSQL;

public static class DbContextFactoryExtensions
{
    public static DbContextOptions<NeoContext> UsePostgreSQL(this DbContextFactory factory, string name)
    {
        var builder = new DbContextOptionsBuilder<NeoContext>();
        builder.UseNpgsql(name);

        return builder.Options;
    }
}