﻿using OCM.Infrastructure.Models;

namespace OCM.Infrastructure.Entities;

public class PlayerOutfitAddonEntity
{
    public PlayerEntity Player { get; set; }
    public int PlayerId { get; set; }
    public int LookType { get; set; }
    public OutfitAddon AddonLevel { get; set; } = OutfitAddon.None;
}