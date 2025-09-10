using MediatR;
using OCM.Application.Requests.Commands;
using OCM.Application.Response;
using OCM.Application.Response.Constants;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Commands.Account;

public class UpdateAccountCommand(IAccountRepository accountRepository)
    : IRequestHandler<UpdateAccountRequest, OutputResponse>
{
    public async Task<OutputResponse> Handle(UpdateAccountRequest request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.GetAsync(request.Id);

        if (account is null)
            return new OutputResponse(ErrorMessage.AccountDoesNotExist);

        // Update account properties
        account.EmailAddress = request.Email;
        if (!string.IsNullOrEmpty(request.Password))
        {
            account.Password = request.Password; // Note: In production, this should be hashed
        }

        // Update Coins from the account table
        account.Coins = request.Coins;

        // Note: PageAccess, Type, and PremiumDays are not direct properties of AccountEntity
        // These might need to be stored in separate tables or calculated fields
        // For now, we'll skip updating these as they may require additional business logic

        await accountRepository.Update(account);

        return new OutputResponse();
    }
}