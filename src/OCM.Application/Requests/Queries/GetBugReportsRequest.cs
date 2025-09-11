using MediatR;
using OCM.Application.Response;
using OCM.Application.Response.BugReports;

namespace OCM.Application.Requests.Queries;

public class GetBugReportsRequest : IRequest<BasePagedResponseViewModel<IEnumerable<BugReportResponseViewModel>>>
{
    public uint? PlayerId { get; set; }
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTo { get; set; }
    public DateTime? ClosedFrom { get; set; }
    public DateTime? ClosedTo { get; set; }
    public string Status { get; set; } // "opened", "closed", "all"
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 10;
}