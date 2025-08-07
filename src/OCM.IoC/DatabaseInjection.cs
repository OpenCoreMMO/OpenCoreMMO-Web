﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OCM.Infrastructure.Contexts;
using OCM.Infrastructure.Factory;
using OCM.Infrastructure.Interfaces;
using OCM.Infrastructure.Providers.InMemory;
using OCM.Infrastructure.Providers.PostgreSQL;
using OCM.Infrastructure.Providers.SQLite;
using OCM.Infrastructure.Repositories;
using OCM.Infrastructure.Repositories.Player;
using Serilog;

namespace OCM.IoC;

public static class DatabaseInjection
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum DatabaseType
    {
        INMEMORY,
        POSTGRESQL,
        SQLITE
    }

    public static IServiceCollection AddRepositories(this IServiceCollection builder)
    {
        builder.AddSingleton<IAccountRepository, AccountRepository>();
        builder.AddSingleton<IAccountPremiumHistoryRepository, AccountPremiumHistoryRepository>();
        builder.AddSingleton<IIpBansRepository, IpBansRepository>();
        builder.AddSingleton<IGuildRepository, GuildRepository>();
        builder.AddSingleton<IPlayerDepotItemRepository, PlayerDepotItemRepository>();
        builder.AddSingleton<IPlayerRepository, PlayerRepository>();
        builder.AddSingleton<IWorldRecordRepository, WorldRecordRepository>();
        builder.AddSingleton<IWorldRepository, WorldRepository>();
        builder.AddSingleton<IPlayerDeathRepository, PlayerDeathRepository>();
        builder.AddSingleton<IReportBugRepository, ReportBugRepository>();
        builder.AddSingleton(typeof(BaseRepository<>));

        return builder;
    }

    public static IServiceCollection AddDatabases(this IServiceCollection builder, IConfiguration configuration)
    {
        return builder.RegisterContext<NeoContext>(configuration);
    }

    private static IServiceCollection RegisterContext<TContext>(this IServiceCollection builder,
        IConfiguration configuration)
        where TContext : DbContext
    {
        DatabaseConfiguration config = new(null, DatabaseType.INMEMORY);

        configuration.GetSection("database").Bind(config);

        LoadEnvironmentVariables(ref config);

        var options = config.Active switch
        {
            DatabaseType.INMEMORY => DbContextFactory.GetInstance()
                .UseInMemory(config.Connections[DatabaseType.INMEMORY]),
            DatabaseType.POSTGRESQL => DbContextFactory.GetInstance()
                .UsePostgreSQL(config.Connections[DatabaseType.POSTGRESQL]),
            DatabaseType.SQLITE => DbContextFactory.GetInstance()
                .UseSQLite(config.Connections[DatabaseType.SQLITE]),
            _ => throw new ArgumentException("Invalid active database!")
        };

        builder.AddSingleton(config);
        builder.AddSingleton(options);

        builder.AddSingleton(x => new NeoContext(options, x.GetRequiredService<ILogger>()) as TContext);

        return builder;
    }

    private static void LoadEnvironmentVariables(ref DatabaseConfiguration databaseConfiguration)
    {
        var activeDatabase = Environment.GetEnvironmentVariable("ACTIVE_DATABASE");
        var sqliteDatabase = Environment.GetEnvironmentVariable("SQLITE_DATABASE");
        var postgresDatabase = Environment.GetEnvironmentVariable("POSTGRESQL_DATABASE");
        var postgresUser = Environment.GetEnvironmentVariable("POSTGRESQL_USER");
        var postgresPassword = Environment.GetEnvironmentVariable("POSTGRESQL_PASSWORD");
        var postgresHost = Environment.GetEnvironmentVariable("POSTGRESQL_HOST");
        var postgresPort = Environment.GetEnvironmentVariable("POSTGRESQL_PORT");

        var dbConnections = databaseConfiguration.Connections;

        if (!string.IsNullOrEmpty(sqliteDatabase))
            dbConnections[DatabaseType.SQLITE] = $"Data Source={sqliteDatabase}";

        if (!string.IsNullOrEmpty(postgresDatabase))
            dbConnections[DatabaseType.POSTGRESQL] =
                $"host={postgresHost};port={postgresPort};database={postgresDatabase};username={postgresUser};password={postgresPassword};";
        var active = databaseConfiguration.Active;

        if (!string.IsNullOrEmpty(activeDatabase) && Enum.TryParse(activeDatabase, out DatabaseType databaseType))
            active = databaseType;

        databaseConfiguration = new DatabaseConfiguration(dbConnections, active);
    }

    public record DatabaseConfiguration(Dictionary<DatabaseType, string> Connections, DatabaseType Active);
}