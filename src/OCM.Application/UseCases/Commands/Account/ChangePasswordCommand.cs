using MediatR;
using OCM.Application.Requests.Commands;
using OCM.Application.Response;
using OCM.Application.Response.Constants;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Commands.Account;

public class ChangePasswordCommand : IRequest<OutputResponse>
{
    public int AccountId { get; set; }
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;

    public static ChangePasswordCommand FromRequest(ChangePasswordRequest request, int accountId)
    {
        return new ChangePasswordCommand
        {
            AccountId = accountId,
            CurrentPassword = request.OldPassword,
            NewPassword = request.NewPassword
        };
    }
}

public class ChangePasswordCommandHandler(IAccountRepository accountRepository) : IRequestHandler<ChangePasswordCommand, OutputResponse>
{
    public async Task<OutputResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.GetById(request.AccountId);

        if (account == null)
        {
            return new OutputResponse(ErrorMessage.AccountDoesNotExist);
        }

        if (account.Password?.Trim() != request.CurrentPassword?.Trim())
        {
            return new OutputResponse(ErrorMessage.InvalidCredentials);
        }

        // Update password
        account.Password = request.NewPassword;
        await accountRepository.Update(account);

        return new OutputResponse(SuccessMessage.PasswordChanged);
    }
}