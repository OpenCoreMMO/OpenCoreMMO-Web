using MediatR;
using OCM.Application.Requests.Queries;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Queries;

public class GetAccountsCountQuery(IAccountRepository accountRepository)
    : IRequestHandler<GetAccountsCountRequest, int>
{
    public async Task<int> Handle(GetAccountsCountRequest request, CancellationToken cancellationToken)
    {
        return await accountRepository.CountAllAsync(_ => true);
    }
}