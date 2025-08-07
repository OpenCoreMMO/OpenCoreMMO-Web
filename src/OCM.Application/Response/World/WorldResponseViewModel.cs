using OCM.Infrastructure.Entities;

namespace OCM.Application.Response.World;

[Serializable]
public class WorldResponseViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Ip { get; set; }
    public int Port { get; set; }

    public Region Region { get; set; }

    public PvpType PvpType { get; set; }

    public WorldType WorldType { get; set; }

    public bool RequiresPremium { get; set; }

    public bool TransferEnabled { get; set; }

    public bool AntiCheatEnabled { get; set; }

    public DateTime CreatedAt { get; set; }

    public int MaxCapacity { get; set; }

    public static implicit operator WorldResponseViewModel(WorldEntity entity)
    {
        return entity == null
            ? null
            : new WorldResponseViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Ip = entity.Ip,
                Port = entity.Port,
                Region = entity.Region,
                PvpType = entity.PvpType,
                WorldType = entity.WorldType,
                RequiresPremium = entity.RequiresPremium,
                TransferEnabled = entity.TransferEnabled,
                AntiCheatEnabled = entity.AntiCheatEnabled,
                CreatedAt = entity.CreatedAt,
                MaxCapacity = entity.MaxCapacity
            };
    }
}