using OCM.Infrastructure.Entities;

namespace OCM.Application.Response;

[Serializable]
public class RoleResponseViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public static implicit operator RoleResponseViewModel(RoleEntity entity)
    {
        return entity == null
            ? null
            : new RoleResponseViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
    }
}