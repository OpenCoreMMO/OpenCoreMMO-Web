using System;
using System.Collections.Generic;

namespace OCM.Infrastructure.Entities;

public sealed class GuildEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int OwnerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Modt { get; set; }
    public ulong BankAmount { get; set; }
    public string Description { get; set; }
    public int Level { get; set; }
    public int Points { get; set; }

    public PlayerEntity Owner { get; set; }
    public ICollection<GuildMembershipEntity> Members { get; set; }
    public ICollection<GuildRankEntity> Ranks { get; set; }
}