﻿using MediatR;
using OCM.Application.Requests.Commands;
using OCM.Application.Response;
using OCM.Application.Response.Constants;
using OCM.Infrastructure.Interfaces;

namespace OCM.Application.UseCases.Commands;

public class UpdatePlayerSkillsCommand(IPlayerRepository playerRepository)
    : IRequestHandler<UpdatePlayerSkillsRequest, OutputResponse>
{
    public async Task<OutputResponse> Handle(UpdatePlayerSkillsRequest request, CancellationToken cancellationToken)
    {
        var entity = await playerRepository.GetAsync(request.Id);

        if (entity is null)
            return new OutputResponse(ErrorMessage.PlayerNotFound);

        entity.SkillAxe = request.SkillAxe;
        entity.SkillDist = request.SkillDist;
        entity.SkillClub = request.SkillClub;
        entity.SkillSword = request.SkillSword;
        entity.SkillShielding = request.SkillShielding;
        entity.SkillFist = request.SkillFist;
        entity.SkillFishing = request.SkillFishing;

        await playerRepository.Update(entity);
        return new OutputResponse();
    }
}