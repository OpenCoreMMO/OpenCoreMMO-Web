﻿using System;

namespace OCM.Infrastructure.Entities;

public sealed class ReportBugEntity
{
    public int Id { get; set; }
    public uint PlayerId { get; set; }
    public string Reason { get; set; }

    public string Ip { get; set; }
    public int PosX { get; set; }
    public int PosY { get; set; }
    public int PosZ { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
}