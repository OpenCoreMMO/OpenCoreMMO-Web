using MediatR;
using OCM.Application.Requests.Queries;
using OCM.Application.Response;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Queries;

public class GetRolesQuery(IRoleRepository roleRepository) : IRequestHandler<GetRolesRequest, BaseResponseViewModel<List<RoleResponseViewModel>>>
{
    public async Task<BaseResponseViewModel<List<RoleResponseViewModel>>> Handle(GetRolesRequest request, CancellationToken cancellationToken)
    {
        var roles = await roleRepository.GetAllAsync();
        var roleResponseViewModels = roles.Select(role => new RoleResponseViewModel
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description
        }).ToList();

        return new BaseResponseViewModel<List<RoleResponseViewModel>>(roleResponseViewModels);
    }
}