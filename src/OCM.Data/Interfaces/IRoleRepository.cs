using System.Threading.Tasks;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Interfaces;

public interface IRoleRepository : IBaseRepositoryNeo<RoleEntity>
{
    Task<RoleEntity> GetByNameAsync(string name);
}