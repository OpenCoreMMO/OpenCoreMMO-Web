using MediatR;
using OCM.Application.Response;

namespace OCM.Application.Requests.Commands;

public class UnbanAccountRequest : IRequest<OutputResponse>, ICommandBase
{
    public int AccountId { get; private set; }

    public void SetAccountId(int accountId)
    {
        AccountId = accountId;
    }
}