using System;

namespace OCM.Infrastructure.Entities;

public class IpBanEntity
{
    public int Id { get; set; }
    public string Ip { get; set; }
    public string Reason { get; set; }
    public DateTime BannedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public int BannedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public int? DeletedBy { get; set; }
}