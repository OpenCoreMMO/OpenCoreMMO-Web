using MediatR;
using OCM.Application.Requests.Commands;
using OCM.Application.Response;
using OCM.Application.Response.Constants;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Commands.Account;

public class LoginCommand : IRequest<OutputResponse>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public static LoginCommand FromRequest(LoginRequest request)
    {
        return new LoginCommand
        {
            Email = request.Email,
            Password = request.Password
        };
    }
}

public class LoginCommandHandler(IAccountRepository accountRepository) : IRequestHandler<LoginCommand, OutputResponse>
{
    public async Task<OutputResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.GetByEmailOrAccountName(request.Email, null);

        if (account == null || account.Password?.Trim() != request.Password?.Trim())
        {
            return new OutputResponse(ErrorMessage.InvalidCredentials);
        }

        // Check if account has sufficient permissions (RoleId != 1 means not a basic Player)
        if (account.RoleId == 1)
        {
            return new OutputResponse(ErrorMessage.InsufficientPermissions);
        }

        // For now, return success with account ID
        return new OutputResponse(account.Id);
    }
}