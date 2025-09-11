using OCM.Infrastructure.Entities;

namespace OCM.Application.Response.Guild;

[Serializable]
public class GuildResponseViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int OwnerId { get; set; }
    public string OwnerName { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Modt { get; set; }
    public ulong BankAmount { get; set; }
    public int MemberCount { get; set; }
    public string Description { get; set; }
    public int Level { get; set; }
    public int Points { get; set; }
    public ICollection<GuildMembershipEntity> Members { get; set; }

    public static implicit operator GuildResponseViewModel(GuildEntity entity)
    {
        return entity == null
            ? null
            : new GuildResponseViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                OwnerId = entity.OwnerId,
                OwnerName = entity.Owner?.Name ?? "Unknown",
                CreatedAt = entity.CreatedAt,
                Modt = entity.Modt,
                BankAmount = entity.BankAmount,
                MemberCount = entity.Members?.Count ?? 0,
                Description = entity.Description,
                Level = entity.Level,
                Points = entity.Points,
                Members = entity.Members
            };
    }
}