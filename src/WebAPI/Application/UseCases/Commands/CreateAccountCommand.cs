﻿using MediatR;
using OCM.Application.Requests.Commands;
using OCM.Application.Response;
using OCM.Application.Response.Constants;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Interfaces;

namespace NeoServer.Web.API.Application.UseCases.Commands;

public class CreateAccountCommand(IAccountRepository accountRepository)
    : IRequestHandler<CreateAccountRequest, OutputResponse>
{
    public async Task<OutputResponse> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        var anotherAccount = await accountRepository.GetByEmailOrAccountName(request.Email, request.AccountName);

        if (anotherAccount?.EmailAddress is not null)
            return new OutputResponse(ErrorMessage.AccountEmailAlreadyExist);

        if (anotherAccount?.AccountName is not null)
            return new OutputResponse(ErrorMessage.AccountNameAlreadyExist);

        var account = new AccountEntity
        {
            Password = request.Password,
            CreatedAt = DateTime.UtcNow,
            EmailAddress = request.Email,
            AccountName = request.AccountName
        };

        await accountRepository.Insert(account);

        return new OutputResponse(account.Id);
    }
}