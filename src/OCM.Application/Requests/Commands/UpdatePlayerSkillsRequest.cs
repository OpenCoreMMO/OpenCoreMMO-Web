﻿using MediatR;
using OCM.Application.Response;

namespace OCM.Application.Requests.Commands;

public class UpdatePlayerSkillsRequest : IRequest<OutputResponse>, ICommandBase
{
    public int Id { get; private set; }

    public int SkillAxe { get; set; }
    public int SkillDist { get; set; }
    public int SkillClub { get; set; }
    public int SkillSword { get; set; }
    public int SkillShielding { get; set; }
    public int SkillFist { get; set; }
    public int SkillFishing { get; set; }
    public int SkillMagicLevel { get; set; }

    public void SetPlayerId(int id)
    {
        Id = id;
    }
}