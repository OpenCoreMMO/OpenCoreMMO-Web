using System;

namespace OCM.Infrastructure.Entities;

public class TownEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int WorldId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public WorldEntity World { get; set; }
}