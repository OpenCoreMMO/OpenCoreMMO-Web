﻿using MediatR;
using OCM.Application.Requests.Commands;
using OCM.Application.Response;
using OCM.Application.Response.Constants;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Commands;

public class AddPremiumDaysAccountCommand(
    IAccountRepository accountRepository,
    IAccountPremiumHistoryRepository accountPremiumHistoryRepository)
    : IRequestHandler<AddPremioumDaysAccountRequest, OutputResponse>
{
    public async Task<OutputResponse> Handle(AddPremioumDaysAccountRequest request, CancellationToken cancellationToken)
    {
        var anotherAccount = await accountRepository.GetAsync(request.AccountId);

        if (anotherAccount is null)
            return new OutputResponse(ErrorMessage.AccountDoesNotExist);

        anotherAccount.PremiumTimeEndAt = anotherAccount.PremiumTimeEndAt.HasValue
            ? anotherAccount.PremiumTimeEndAt.Value.AddDays(request.Days)
            : DateTime.UtcNow.AddDays(request.Days);

        var accountPremiumHistory = new AccountPremiumHistoryEntity
        {
            AccountId = anotherAccount.Id,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
            EndAt = anotherAccount.PremiumTimeEndAt.Value
        };

        var updateAccountTask = accountRepository.Update(anotherAccount);
        var insertAccountPremiumHistory = accountPremiumHistoryRepository.Insert(accountPremiumHistory);
        await Task.WhenAll(updateAccountTask, insertAccountPremiumHistory);

        return new OutputResponse();
    }
}