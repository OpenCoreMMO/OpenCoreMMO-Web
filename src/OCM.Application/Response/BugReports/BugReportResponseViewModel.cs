using OCM.Infrastructure.Entities;

namespace OCM.Application.Response.BugReports;

[Serializable]
public class BugReportResponseViewModel
{
    public int Id { get; set; }
    public uint PlayerId { get; set; }
    public string PlayerName { get; set; }
    public string Reason { get; set; }
    public string Ip { get; set; }
    public int PosX { get; set; }
    public int PosY { get; set; }
    public int PosZ { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public bool IsClosed => ClosedAt.HasValue;

    public static implicit operator BugReportResponseViewModel(ReportBugEntity entity)
    {
        return entity == null
            ? null
            : new BugReportResponseViewModel
            {
                Id = entity.Id,
                PlayerId = entity.PlayerId,
                Reason = entity.Reason,
                Ip = entity.Ip,
                PosX = entity.PosX,
                PosY = entity.PosY,
                PosZ = entity.PosZ,
                CreatedAt = entity.CreatedAt,
                ClosedAt = entity.ClosedAt
            };
    }
}