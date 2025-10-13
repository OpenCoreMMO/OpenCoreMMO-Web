using MediatR;
using OCM.Application.Response;

namespace OCM.Application.Requests.Commands;

public class ChangePasswordRequest : IRequest<OutputResponse>
{
    public int AccountId { get; set; }
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}