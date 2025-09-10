using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Models;

namespace OCM.Application.Response.Player;

[Serializable]
public class PlayerResponseViewModel
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int TownId { get; set; }
    public string Name { get; set; }
    public byte Group { get; set; }
    public uint Capacity { get; set; }
    public ushort Level { get; set; }
    public ushort Mana { get; set; }
    public ushort MaxMana { get; set; }
    public uint Health { get; set; }
    public uint MaxHealth { get; set; }
    public byte Soul { get; set; }
    public byte MaxSoul { get; set; }
    public ushort Speed { get; set; }
    public ushort StaminaMinutes { get; set; }
    public bool Online { get; set; }

    public int LookAddons { get; set; }
    public int LookBody { get; set; }
    public int LookFeet { get; set; }
    public int LookHead { get; set; }
    public int LookLegs { get; set; }
    public int LookType { get; set; }

    public int PosX { get; set; }
    public int PosY { get; set; }
    public int PosZ { get; set; }
    public int SkillFist { get; set; }
    public double SkillFistTries { get; set; }

    public int SkillClub { get; set; }
    public double SkillClubTries { get; set; }

    public int SkillSword { get; set; }
    public double SkillSwordTries { get; set; }

    public int SkillAxe { get; set; }
    public double SkillAxeTries { get; set; }

    public int SkillDist { get; set; }
    public double SkillDistTries { get; set; }

    public int SkillShielding { get; set; }
    public double SkillShieldingTries { get; set; }

    public int SkillFishing { get; set; }
    public double SkillFishingTries { get; set; }

    public int MagicLevel { get; set; }
    public double MagicLevelTries { get; set; }
    public double Experience { get; set; }

    public ChaseMode ChaseMode { get; set; }
    public FightMode FightMode { get; set; }
    public Gender Gender { get; set; }
    public byte Vocation { get; set; }
    public int RemainingRecoverySeconds { get; set; }
    public int WorldId { get; set; }

    public static implicit operator PlayerResponseViewModel(PlayerEntity entity)
    {
        return entity == null
            ? null
            : new PlayerResponseViewModel
            {
                Id = entity.Id,
                AccountId = entity.AccountId,
                TownId = entity.TownId,
                Name = entity.Name,
                Group = entity.Group,
                Capacity = entity.Capacity,
                Level = entity.Level,
                Mana = (ushort)entity.Mana,
                MaxMana = (ushort)entity.MaxMana,
                Health = entity.Health,
                MaxHealth = entity.MaxHealth,
                Soul = entity.Soul,
                MaxSoul = entity.MaxSoul,
                Speed = entity.Speed,
                StaminaMinutes = entity.StaminaMinutes,
                Online = entity.Online,

                LookAddons = entity.LookAddons,
                LookBody = entity.LookBody,
                LookFeet = entity.LookFeet,
                LookHead = entity.LookHead,
                LookLegs = entity.LookLegs,
                LookType = entity.LookType,

                PosX = entity.PosX,
                PosY = entity.PosY,
                PosZ = entity.PosZ,

                SkillFist = entity.SkillFist,
                SkillFistTries = entity.SkillFistTries,

                SkillClub = entity.SkillClub,
                SkillClubTries = entity.SkillClubTries,

                SkillSword = entity.SkillSword,
                SkillSwordTries = entity.SkillSwordTries,

                SkillAxe = entity.SkillAxe,
                SkillAxeTries = entity.SkillAxeTries,

                SkillDist = entity.SkillDist,
                SkillDistTries = entity.SkillDistTries,

                SkillShielding = entity.SkillShielding,
                SkillShieldingTries = entity.SkillShieldingTries,

                SkillFishing = entity.SkillFishing,
                SkillFishingTries = entity.SkillFishingTries,

                MagicLevel = entity.MagicLevel,
                MagicLevelTries = entity.MagicLevelTries,

                Experience = entity.Experience,
                ChaseMode = entity.ChaseMode,
                FightMode = entity.FightMode,
                Gender = entity.Gender,
                Vocation = entity.Vocation,
                RemainingRecoverySeconds = entity.RemainingRecoverySeconds,
                WorldId = entity.WorldId
            };
    }
}