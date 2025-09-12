using MediatR;
using OCM.Application.Response;
using System.ComponentModel.DataAnnotations;

namespace OCM.Application.Requests.Commands;

public class BanIpRequest : IRequest<OutputResponse>, ICommandBase
{
    [Required(ErrorMessage = "IP address is required.")]
    [StringLength(45, ErrorMessage = "IP address cannot exceed 45 characters.")]
    [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", ErrorMessage = "IP address must be a valid IPv4 address.")]
    public string Ip { get; set; }

    [Required(ErrorMessage = "Reason is required.")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Reason must be between 10 and 500 characters.")]
    public string Reason { get; set; }

    [Required(ErrorMessage = "Days is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Days must be greater than 0.")]
    public int Days { get; set; }
}