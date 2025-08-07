using MediatR;
using OCM.Application.Response.IpBans;

namespace OCM.Application.Requests.Queries;

public record GetIpBanByIpRequest(string Ip) : IRequest<IpBanResponseViewModel>;