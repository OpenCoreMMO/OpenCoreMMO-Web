using MediatR;
using OCM.Application.Response;
using OCM.Application.Response.IpBans;

namespace OCM.Application.Requests.Queries;

public class GetIpBansRequest : IRequest<BasePagedResponseViewModel<IEnumerable<IpBanResponseViewModel>>>
{
    public string IpAddress { get; set; }
    public uint? BannedById { get; set; }
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTo { get; set; }
    public DateTime? ExpiresFrom { get; set; }
    public DateTime? ExpiresTo { get; set; }
    public string Status { get; set; } // "active", "expired", "all"
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 10;
}