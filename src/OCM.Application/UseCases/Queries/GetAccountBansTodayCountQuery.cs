using MediatR;
using OCM.Application.Requests.Queries;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Queries;

public class GetAccountBansTodayCountQuery(IAccountRepository accountRepository)
    : IRequestHandler<GetAccountBansTodayCountRequest, int>
{
    public async Task<int> Handle(GetAccountBansTodayCountRequest request, CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow.Date;
        return await accountRepository.CountAllAsync(a => a.BanishedAt.HasValue && a.BanishedAt.Value.Date == today);
    }
}