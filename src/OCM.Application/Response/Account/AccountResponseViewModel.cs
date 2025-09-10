using OCM.Infrastructure.Entities;

namespace OCM.Application.Response.Account;

[Serializable]
public class AccountResponseViewModel
{
    public int Id { get; set; }
    public string Email { get; set; }
    public int PageAccess { get; set; }
    public int Coins { get; set; }

    public static implicit operator AccountResponseViewModel(AccountEntity entity)
    {
        return entity == null
            ? null
            : new AccountResponseViewModel
            {
                Id = entity.Id,
                Email = entity.EmailAddress,
                PageAccess = 0, // todo: implement
                Coins = entity.Players?.Sum(p => (int)p.BankAmount) ?? 0
            };
    }
}