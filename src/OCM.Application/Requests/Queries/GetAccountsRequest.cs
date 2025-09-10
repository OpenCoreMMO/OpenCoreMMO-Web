using MediatR;
using OCM.Application.Response;
using OCM.Application.Response.Account;

namespace OCM.Application.Requests.Queries;

public class GetAccountsRequest : IRequest<BasePagedResponseViewModel<IEnumerable<AccountResponseViewModel>>>
{
    public string Email { get; set; }
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 10;
}