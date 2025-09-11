using MediatR;
using OCM.Application.Requests.Commands;
using OCM.Application.Response;
using OCM.Application.Response.Constants;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Commands.Account;

public class UnbanAccountCommand(IAccountRepository accountRepository)
    : IRequestHandler<UnbanAccountRequest, OutputResponse>
{
    public async Task<OutputResponse> Handle(UnbanAccountRequest request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.GetAsync(request.AccountId);

        if (account is null)
            return new OutputResponse(ErrorMessage.AccountDoesNotExist);

        if (account.BanishedAt is null)
            return new OutputResponse("Account is not banned");

        account.BanishmentReason = null;
        account.BanishedAt = null;
        account.BanishedEndAt = null;
        account.BannedBy = null;

        await accountRepository.Update(account);

        return new OutputResponse();
    }
}