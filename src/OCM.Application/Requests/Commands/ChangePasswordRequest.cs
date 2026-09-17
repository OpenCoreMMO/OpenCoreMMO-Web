using MediatR;
using OCM.Application.Response;

namespace OCM.Application.Requests.Commands;

public class ChangePasswordRequest : IRequest<OutputResponse>, ICommandBase
{
    public int AccountId { get; private set; }
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;

    public void SetAccountId(int accountId)
    {
        AccountId = accountId;
    }
}