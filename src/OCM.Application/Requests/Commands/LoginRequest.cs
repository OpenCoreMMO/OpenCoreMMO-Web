using MediatR;
using OCM.Application.Response;

namespace OCM.Application.Requests.Commands;

public class LoginRequest : IRequest<OutputResponse>, ICommandBase
{
    public string Email { get; set; }
    public string Password { get; set; }
}