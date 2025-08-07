﻿using System.Collections.Generic;

namespace OCM.Infrastructure.Entities;

public sealed class PlayerInventoryItemEntity
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public int ServerId { get; set; }
    public int SlotId { get; set; }
    public short Amount { get; set; }

    public Dictionary<string, string> Attributes { get; set; } = new();

    public PlayerEntity Player { get; set; }
}