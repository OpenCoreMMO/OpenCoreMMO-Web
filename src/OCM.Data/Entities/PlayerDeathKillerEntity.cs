namespace OCM.Infrastructure.Entities;

public class PlayerDeathKillerEntity
{
    public int Id { get; set; }
    public int PlayerDeathId { get; set; }
    public PlayerEntity Player { get; set; }
    public int? PlayerId { get; set; }
    public string KillerName { get; set; }
    public int Damage { get; set; }
    public PlayerDeathEntity PlayerDeath { get; set; }
}