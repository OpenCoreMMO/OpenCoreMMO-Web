using MediatR;
using OCM.Application.Requests.Commands;
using OCM.Application.Response;
using OCM.Application.Response.Constants;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Commands;

public class CloseBugCommand(IReportBugRepository reportBugRepository) : IRequestHandler<CloseBugRequest, OutputResponse>
{
    public async Task<OutputResponse> Handle(CloseBugRequest request, CancellationToken cancellationToken)
    {
        var bugReport = await reportBugRepository.GetAsync(request.BugReportId);

        if (bugReport == null)
            return new OutputResponse(ErrorMessage.BugReportNotFound);

        if (bugReport.ClosedAt.HasValue)
            return new OutputResponse(ErrorMessage.BugReportAlreadyClosed);

        bugReport.ClosedAt = DateTime.UtcNow;
        await reportBugRepository.Update(bugReport);

        return new OutputResponse();
    }
}