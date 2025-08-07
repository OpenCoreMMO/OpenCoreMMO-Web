using MediatR;
using OCM.Application.Response;

namespace OCM.Application.Requests.Commands;

public class AddPremioumDaysAccountRequest : IRequest<OutputResponse>, ICommandBase
{
    public int Days { get; set; }
    public string Description { get; set; }
    public int AccountId { get; private set; }


    public void SetAccountId(int accountId)
    {
        AccountId = accountId;
    }
}