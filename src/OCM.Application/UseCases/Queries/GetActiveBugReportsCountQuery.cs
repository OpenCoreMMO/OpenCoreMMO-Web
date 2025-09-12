using MediatR;
using OCM.Application.Requests.Queries;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Queries;

public class GetActiveBugReportsCountQuery(IReportBugRepository reportBugRepository)
    : IRequestHandler<GetActiveBugReportsCountRequest, int>
{
    public async Task<int> Handle(GetActiveBugReportsCountRequest request, CancellationToken cancellationToken)
    {
        return await reportBugRepository.CountAllAsync(r => r.ClosedAt == null);
    }
}