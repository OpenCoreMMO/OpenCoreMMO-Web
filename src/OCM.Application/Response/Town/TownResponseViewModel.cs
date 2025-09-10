using OCM.Infrastructure.Entities;

namespace OCM.Application.Response.Town;

[Serializable]
public class TownResponseViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int WorldId { get; set; }
    public DateTime CreatedAt { get; set; }

    public static implicit operator TownResponseViewModel(TownEntity entity)
    {
        return entity == null
            ? null
            : new TownResponseViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                WorldId = entity.WorldId,
                CreatedAt = entity.CreatedAt
            };
    }
}