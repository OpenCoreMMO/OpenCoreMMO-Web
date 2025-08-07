using MediatR;
using OCM.Application.Response;

namespace OCM.Application.Requests.Commands;

public class BanAccountRequest : IRequest<OutputResponse>, ICommandBase
{
    public int AccountId { get; private set; }

    public string Reason { get; set; }

    public int Days { get; set; }

    public void SetAccountId(int accountId)
    {
        AccountId = accountId;
    }
}