using MediatR;
using OCM.Application.Response;

namespace OCM.Application.Requests.Commands;

public class CloseBugRequest : IRequest<OutputResponse>, ICommandBase
{
    public int BugReportId { get; private set; }

    public void SetBugReportId(int bugReportId)
    {
        BugReportId = bugReportId;
    }
}