using System.Threading.Tasks;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Interfaces;

public interface IIpBansRepository : IBaseRepositoryNeo<IpBanEntity>
{
    Task<IpBanEntity> ExistBan(string Ip);
}