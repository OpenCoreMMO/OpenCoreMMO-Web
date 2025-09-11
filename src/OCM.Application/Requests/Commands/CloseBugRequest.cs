using MediatR;
using OCM.Application.Response;

namespace OCM.Application.Requests.Commands;

public class CloseBugRequest : IRequest<OutputResponse>, ICommandBase
{
    public long BugReportId { get; private set; }

    public void SetBugReportId(long bugReportId)
    {
        BugReportId = bugReportId;
    }
}