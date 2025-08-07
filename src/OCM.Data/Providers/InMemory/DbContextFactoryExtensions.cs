using Microsoft.EntityFrameworkCore;
using OCM.Infrastructure.Contexts;
using OCM.Infrastructure.Factory;

namespace OCM.Infrastructure.Providers.InMemory;

public static class DbContextFactoryExtensions
{
    public static DbContextOptions<NeoContext> UseInMemory(this DbContextFactory factory, string name)
    {
        var builder = new DbContextOptionsBuilder<NeoContext>();
        builder.UseInMemoryDatabase(name);

        return builder.Options;
    }
}