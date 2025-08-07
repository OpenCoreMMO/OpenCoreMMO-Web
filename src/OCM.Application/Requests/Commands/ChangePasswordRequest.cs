using MediatR;
using OCM.Application.Response;

namespace OCM.Application.Requests.Commands;

public class ChangePasswordRequest : IRequest<OutputResponse>, ICommandBase
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }

    public int AccountId { get; private set; }

    public void SetAccountId(int accountId)
    {
        AccountId = accountId;
    }
}