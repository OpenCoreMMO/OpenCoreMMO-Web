using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OCM.Application.Response;
using OCM.Application.Response.World;
using OCM.Infrastructure.Entities;

namespace OCM.Application.Requests.Queries;

public class GetWorldsRequest : IRequest<BasePagedResponseViewModel<IEnumerable<WorldResponseViewModel>>>
{
    public string Name { get; set; }
    public bool? RequiresPremium { get; set; }
    public bool? TransferEnabled { get; set; }
    public bool? AntiCheatEnabled { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public Region? Continent { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public PvpType? PvpType { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public WorldType? Type { get; set; }

    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 5;
}