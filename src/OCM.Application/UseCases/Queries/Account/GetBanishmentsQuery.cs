using System.Linq.Expressions;
using MediatR;
using OCM.Application.Requests.Queries;
using OCM.Application.Response;
using OCM.Application.Response.Account;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Queries.Account;

public class GetBanishmentsQuery(IAccountRepository accountRepository)
    : IRequestHandler<GetBanishmentsRequest, BasePagedResponseViewModel<IEnumerable<AccountResponseViewModel>>>
{
    public async Task<BasePagedResponseViewModel<IEnumerable<AccountResponseViewModel>>> Handle(
        GetBanishmentsRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<AccountEntity, bool>> expression = item =>
            item.BanishedAt.HasValue &&
            (string.IsNullOrEmpty(request.Email) || item.EmailAddress.ToLower().Contains(request.Email.ToLower()));

        var totalAccounts = await accountRepository.CountAllAsync(expression);
        var accounts = await accountRepository.GetPaginatedAccountsAsync(expression, request.Page, request.Limit);
        var response = accounts.Select(item => (AccountResponseViewModel)item);

        var totalPages = (int)Math.Ceiling((double)totalAccounts / request.Limit);

        return new BasePagedResponseViewModel<IEnumerable<AccountResponseViewModel>>(response, request.Page,
            request.Limit, totalAccounts, totalPages);
    }
}