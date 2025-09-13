using MediatR;
using OCM.Application.Response;

namespace OCM.Application.Requests.Commands;

public class CreateAccountRequest : IRequest<OutputResponse>, ICommandBase
{
    public string Password { get; set; }
    public string Email { get; set; }
    public string AccountName { get; set; }
    public int Coins { get; set; }
    public int RoleId { get; set; } = 1; // Default to Player role
}