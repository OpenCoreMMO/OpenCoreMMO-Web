using OCM.Infrastructure.Entities;

namespace OCM.Application.Response.IpBans;

[Serializable]
public class IpBanResponseViewModel
{
    public int Id { get; set; }
    public string IpAddress { get; set; }
    public string Reason { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public uint? BannedById { get; set; }
    public string BannedByName { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsActive { get; set; }

    public static implicit operator IpBanResponseViewModel(IpBanEntity entity)
    {
        return entity == null
            ? null
            : new IpBanResponseViewModel
            {
                Id = entity.Id,
                IpAddress = entity.Ip,
                Reason = entity.Reason,
                CreatedAt = entity.BannedAt,
                ExpiresAt = entity.ExpiresAt == DateTime.MaxValue ? null : entity.ExpiresAt,
                BannedById = (uint?)entity.BannedBy,
                BannedByName = "", // Will be populated in query
                DeletedAt = entity.DeletedAt,
                IsActive = entity.ExpiresAt > DateTime.UtcNow
            };
    }
}