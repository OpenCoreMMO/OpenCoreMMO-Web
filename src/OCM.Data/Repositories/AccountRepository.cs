﻿using System;
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

public class AccountRepository : BaseRepository<AccountEntity>, IAccountRepository
{
    #region constructors

    public AccountRepository(DbContextOptions<NeoContext> contextOptions, ILogger logger) : base(contextOptions,
        logger)
    {
    }

    #endregion

    #region public methods implementation

    #region gets

    public async Task<IList<AccountEntity>> GetPaginatedAccountsAsync(Expression<Func<AccountEntity, bool>> filter,
        int page, int limit)
    {
        await using var context = NewDbContext;
        var skip = (page - 1) * limit;
        return await context.Accounts
            .Where(filter)
            .Include(x => x.Players)
            .Skip(skip)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<AccountEntity> GetAccount(string name, string password)
    {
        await using var context = NewDbContext;

        return await context.Accounts
            .Where(x => (x.EmailAddress.Equals(name) || x.AccountName.Equals(name)) && x.Password.Equals(password))
            .Include(x => x.Players)
            .ThenInclude(x => x.World)
            .SingleOrDefaultAsync();
    }

    public async Task<PlayerEntity> GetPlayer(string accountName, string password, string charName,
        bool includeDeathList = false, bool includeKillsLastMonth = false)
    {
        await using var context = NewDbContext;

        var query = context.Players.Where(x => x.Account.EmailAddress.Equals(accountName) &&
                                               x.Account.Password.Equals(password) &&
                                               x.Name.Equals(charName))
            .Include(x => x.PlayerItems)
            .Include(x => x.PlayerInventoryItems)
            .Include(x => x.World)
            .Include(x => x.Account)
            .ThenInclude(x => x.VipList)
            .ThenInclude(x => x.Player)
            .Include(x => x.GuildMember)
            .ThenInclude(x => x.Guild)
            .Include(x => x.PlayerStorages);

        if (includeDeathList)
            query.Include(x => x.Deaths)
                .ThenInclude(x => x.Killers);

        var result = await query.AsNoTracking().SingleOrDefaultAsync();

        if (result is null) return null;

        if (includeKillsLastMonth)
        {
            var lastMonth = DateTime.Now.AddMonths(-1).ToUniversalTime();
            result.KillsLastMonth = await context.PlayerDeathKillers
                .Include(x => x.PlayerDeath)
                .Where(x => x.PlayerId == result.Id && x.PlayerDeath.DeathDateTime >= lastMonth)
                .Select(x => x.PlayerDeath)
                .AsNoTracking()
                .ToListAsync();
        }

        return result;
    }

    public async Task<PlayerEntity> GetOnlinePlayer(string accountName)
    {
        await using var context = NewDbContext;

        return await context.Players
            .Include(x => x.Account)
            .Where(x => x.Account.EmailAddress.Equals(accountName) && x.Online)
            .FirstOrDefaultAsync();
    }

    public async Task<AccountEntity> GetByEmailOrAccountName(string email, string accountName)
    {
        await using var context = NewDbContext;

        return await context.Accounts
            .Where(x => (email != null && x.EmailAddress.ToLower().Equals(email.ToLower())) ||
                        (accountName != null && x.AccountName.ToLower().Equals(accountName.ToLower())))
            .SingleOrDefaultAsync();
    }
    
    public async Task<AccountEntity> GetById(int accountId)
    {
        await using var context = NewDbContext;

        return await context.Accounts
            .Where(x => x.Id == accountId)
            .SingleOrDefaultAsync();
    }

    #endregion

    #region inserts

    public async Task AddPlayerToVipList(int accountId, int playerId)
    {
        await using var context = NewDbContext;

        await context.AccountsVipList.AddAsync(new AccountVipListEntity
        {
            AccountId = accountId,
            PlayerId = playerId
        });

        await CommitChanges(context);
    }

    #endregion

    #region updates

    public async Task<int> Ban(uint accountId, string reason, uint bannedByAccountId)
    {
        await using var context = NewDbContext;

        return await context.Accounts
            .Where(x => x.Id == accountId)
            .ExecuteUpdateAsync(x
                => x.SetProperty(y => y.BannedBy, bannedByAccountId)
                    .SetProperty(y => y.BanishmentReason, reason)
                    .SetProperty(y => y.BanishedAt, DateTime.Now));
    }

    #endregion

    #region deletes

    public async Task RemoveFromVipList(int accountId, int playerId)
    {
        await using var context = NewDbContext;

        var item = await context.AccountsVipList.SingleOrDefaultAsync(x =>
            x.PlayerId == playerId && x.AccountId == accountId);

        if (item is null) return;

        context.AccountsVipList.Remove(item);
        await CommitChanges(context);
    }

    #endregion

    #endregion
}