using FluentValidation;
using OCM.Application.Requests.Commands;
using System.Net;
using System.Text.RegularExpressions;

namespace OCM.Application.Requests.Validators;

public class BanIpValidator : AbstractValidator<BanIpRequest>
{
    public BanIpValidator()
    {
        RuleFor(x => x.Ip)
            .NotEmpty().WithMessage("IP address is required.")
            .MaximumLength(45).WithMessage("IP address cannot exceed 45 characters.")
            .Must(BeValidIPv4).WithMessage("IP address must be a valid IPv4 address.");

        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage("Reason is required.")
            .MinimumLength(10).WithMessage("Reason must be at least 10 characters long.")
            .MaximumLength(500).WithMessage("Reason cannot exceed 500 characters.");

        RuleFor(x => x.Days)
            .NotEmpty().WithMessage("Days is required.")
            .GreaterThan(0).WithMessage("Days must be greater than 0.");
    }

    private bool BeValidIPv4(string ip)
    {
        if (string.IsNullOrWhiteSpace(ip))
            return false;

        // Check if it's a valid IPv4 address
        return IPAddress.TryParse(ip, out var address) && address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork;
    }
}