using MediatR;
using OCM.Application.Response;

namespace OCM.Application.Requests.Commands.Guild;

public class UpdateGuildRequest : IRequest<OutputResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Level { get; set; }
    public int Points { get; set; }
    public ulong BankAmount { get; set; }
}