using MediatR;
using OCM.Application.Response;

namespace OCM.Application.Requests.Queries;

public class GetRolesRequest : IRequest<BaseResponseViewModel<List<RoleResponseViewModel>>>
{
}