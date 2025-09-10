using MediatR;
using OCM.Application.Requests.Commands;
using OCM.Application.Response;
using OCM.Application.Response.Constants;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Commands;

public class ChangePasswordAccountCommand(IAccountRepository accountRepository)
    : IRequestHandler<ChangePasswordRequest, OutputResponse>
{
    public async Task<OutputResponse> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var anotherAccount = await accountRepository.GetAsync(request.AccountId);

        if (anotherAccount is null)
            return new OutputResponse(ErrorMessage.AccountDoesNotExist);

        if (anotherAccount.Password.Trim() != request.OldPassword) // TODO: Hash the password
            return new OutputResponse(ErrorMessage.AccountInvalidPassword);

        anotherAccount.Password = request.NewPassword;

        await accountRepository.Update(anotherAccount);

        return new OutputResponse();
    }
}