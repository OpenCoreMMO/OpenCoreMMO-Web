using MediatR;
using OCM.Application.Response;

namespace OCM.Application.Requests.Commands;

public class UpdateAccountRequest : IRequest<OutputResponse>
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int PageAccess { get; set; }
    public int Type { get; set; }
    public int PremiumDays { get; set; }
    public int Coins { get; set; }
    public int RoleId { get; set; }
}