using MediatR;
using OCM.Application.Response;
using OCM.Infrastructure.Entities;

namespace OCM.Application.Requests.Commands;

public class UpdateWorldRequest : IRequest<OutputResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Ip { get; set; }
    public int Port { get; set; }

    public Region Region { get; set; }

    public PvpType PvpType { get; set; }

    public WorldType WorldType { get; set; }

    public bool RequiresPremium { get; set; }

    public bool TransferEnabled { get; set; }

    public bool AntiCheatEnabled { get; set; }
    public int MaxCapacity { get; set; }
}