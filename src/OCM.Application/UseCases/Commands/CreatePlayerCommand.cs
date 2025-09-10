using MediatR;
using Microsoft.Extensions.Options;
using NeoServer.Web.API.IoC.Configs;
using OCM.Application.Requests.Commands;
using OCM.Application.Response;
using OCM.Application.Response.Constants;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Interfaces;
using OCM.Infrastructure.Models;
using System;

namespace OCM.Application.UseCases.Commands;

public class CreatePlayerCommand(IPlayerRepository playerRepository, IOptions<PlayerConfig> config)
    : IRequestHandler<CreatePlayerRequest, OutputResponse>
{
    public async Task<OutputResponse> Handle(CreatePlayerRequest request, CancellationToken cancellationToken)
    {
        var playerAlreadyExist = await playerRepository.GetByName(request.Name);

        if (playerAlreadyExist is not null)
            return new OutputResponse(ErrorMessage.PlayerAlreadyExist);

        // Calculate stats based on level and vocation
        var maxHealth = CalculateMaxHealth(request.Vocation, request.Level);
        var maxMana = CalculateMaxMana(request.Vocation, request.Level);
        var maxSoul = CalculateMaxSoul(request.Vocation);
        var capacity = CalculateCapacity(request.Level);
        var experience = CalculateExperience(request.Level);
        var speed = CalculateSpeed(request.Level);

        var player = new PlayerEntity
        {
            AccountId = request.AccountId,
            Level = (ushort)request.Level,
            Capacity = capacity,
            Experience = experience,
            Gender = (Gender)request.Sex,
            WorldId = request.WorldId,
            Health = maxHealth,
            MaxHealth = maxHealth,
            Mana = maxMana,
            MaxMana = maxMana,
            Soul = maxSoul,
            Speed = speed,
            Name = request.Name,
            FightMode = config.Value.FightMode,
            LookType = config.Value.LookType,
            LookBody = config.Value.LookBody,
            LookFeet = config.Value.LookFeet,
            LookHead = config.Value.LookHead,
            LookLegs = config.Value.LookLegs,
            MagicLevel = config.Value.MagicLevel,
            Vocation = (byte)request.Vocation,
            ChaseMode = config.Value.ChaseMode,
            MaxSoul = maxSoul,
            Group = config.Value.Group,
            PosX = request.PosX,
            PosY = request.PosY,
            PosZ = request.PosZ,
            SkillAxe = config.Value.SkillAxe,
            SkillDist = config.Value.SkillDist,
            SkillClub = config.Value.SkillClub,
            SkillSword = config.Value.SkillSword,
            SkillShielding = config.Value.SkillShielding,
            SkillFist = config.Value.SkillFist,
            TownId = request.Town,
            SkillFishing = config.Value.SkillFishing,
            BankAmount = request.BankAmount
        };

        await playerRepository.Add(player);

        return new OutputResponse(player.Id);
    }

    private uint CalculateMaxHealth(int vocation, int level)
    {
        // Base HP for levels 1-8 is 185
        if (level <= 8) return 185;

        uint baseHp = 185;
        int levelBonus = level - 8;

        return vocation switch
        {
            1 => baseHp + (uint)(15 * levelBonus), // Knight
            2 => baseHp + (uint)(10 * levelBonus), // Paladin
            3 => baseHp + (uint)(5 * levelBonus),  // Druid
            4 => baseHp + (uint)(5 * levelBonus),  // Sorcerer
            _ => baseHp
        };
    }

    private uint CalculateMaxMana(int vocation, int level)
    {
        // Base Mana for levels 1-8 is 35
        if (level <= 8) return 35;

        uint baseMana = 35;
        int levelBonus = level - 8;

        return vocation switch
        {
            1 => baseMana + (uint)(5 * levelBonus),  // Knight
            2 => baseMana + (uint)(15 * levelBonus), // Paladin
            3 => baseMana + (uint)(30 * levelBonus), // Druid
            4 => baseMana + (uint)(30 * levelBonus), // Sorcerer
            _ => baseMana
        };
    }

    private byte CalculateMaxSoul(int vocation)
    {
        return vocation switch
        {
            1 => (byte)100, // Knight
            2 => (byte)200, // Paladin
            3 => (byte)200, // Druid
            4 => (byte)200, // Sorcerer
            _ => (byte)100
        };
    }

    private uint CalculateCapacity(int level)
    {
        // Base capacity for levels 1-8 is 470
        if (level <= 8) return 470;

        return (uint)(470 + (10 * (level - 8)));
    }

    private long CalculateExperience(int level)
    {
        if (level <= 1) return 0;

        // Exp(Level) = (50 × (Level³ - Level)) / 3
        long levelCubed = (long)level * level * level;
        return (50 * (levelCubed - level)) / 3;
    }

    private ushort CalculateSpeed(int level)
    {
        // Speed = 220 + (2 × (Level - 1))
        return (ushort)(220 + (2 * (level - 1)));
    }
}