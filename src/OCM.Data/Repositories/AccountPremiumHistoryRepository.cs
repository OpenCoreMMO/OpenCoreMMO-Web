using Microsoft.EntityFrameworkCore;
using OCM.Infrastructure.Contexts;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Interfaces;
using Serilog;

namespace OCM.Infrastructure.Repositories;

public class AccountPremiumHistoryRepository(DbContextOptions<NeoContext> contextOptions, ILogger logger)
    : BaseRepository<AccountPremiumHistoryEntity>(contextOptions,
        logger), IAccountPremiumHistoryRepository;