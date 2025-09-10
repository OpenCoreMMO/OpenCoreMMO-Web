using OCM.Infrastructure.Entities;

namespace OCM.Application.Response.Account;

[Serializable]
public class AccountResponseViewModel
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int PageAccess { get; set; }
    public int Type { get; set; }
    public int PremiumDays { get; set; }
    public int Coins { get; set; }
    public DateTime? BanishedAt { get; set; }
    public DateTime? BanishedEndAt { get; set; }
    public string BanishmentReason { get; set; }
    public uint? BannedBy { get; set; }

    public static implicit operator AccountResponseViewModel(AccountEntity entity)
    {
        return entity == null
            ? null
            : new AccountResponseViewModel
            {
                Id = entity.Id,
                Email = entity.EmailAddress,
                Password = entity.Password,
                PageAccess = 0, // todo: implement
                Type = 0, // todo: implement
                PremiumDays = entity.PremiumTimeEndAt.HasValue
                    ? (int)(entity.PremiumTimeEndAt.Value - DateTime.Now).TotalDays
                    : 0,
                Coins = entity.Coins,
                BanishedAt = entity.BanishedAt,
                BanishedEndAt = entity.BanishedEndAt,
                BanishmentReason = entity.BanishmentReason,
                BannedBy = entity.BannedBy
            };
    }
}