using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Models;
using OCM.Application.Helpers;

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
    public string VocationName { get; set; }
    public int RemainingRecoverySeconds { get; set; }
    public int HealthPercentage { get; set; }
    public int ManaPercentage { get; set; }
    public int LevelPercentage { get; set; }
    public ulong Balance { get; set; }
    public string GroupName { get; set; }
    public string Sex { get; set; }
    public string WorldName { get; set; }
    public string TownName { get; set; }
    public string DailyReward { get; set; }
    public string Tutorial { get; set; }
    public string Blessings { get; set; }
    public int WorldId { get; set; }
    public long ExperienceToNextLevel { get; set; }

    private static string GetVocationName(byte vocation)
    {
        return VocationConstants.GetVocationName(vocation);
    }

    private static string GetGroupName(byte group)
    {
        return group switch
        {
            1 => "Player",
            2 => "Tutor",
            3 => "Senior Tutor",
            4 => "Gamemaster",
            5 => "Community Manager",
            6 => "God",
            _ => "Unknown"
        };
    }

    private static string GetSexName(Gender gender)
    {
        return gender switch
        {
            Gender.Male => "Male",
            Gender.Female => "Female",
            _ => "Unknown"
        };
    }

    private static int CalculateHealthPercentage(uint health, uint maxHealth)
    {
        return maxHealth == 0 ? 0 : (int)((double)health / maxHealth * 100);
    }

    private static int CalculateManaPercentage(uint mana, uint maxMana)
    {
        return maxMana == 0 ? 0 : (int)((double)mana / maxMana * 100);
    }

    private static int CalculateLevelPercentage(double experience, ushort level)
    {
        // Calculate experience needed for current level
        long expForCurrentLevel = CalculateExperienceForLevel(level);

        // If player has less experience than needed for current level,
        // treat them as if they're progressing from level 1
        if (experience < expForCurrentLevel)
        {
            // Use level 1 as baseline
            expForCurrentLevel = 0;
            level = 1;
        }

        // Calculate experience needed for next level
        long expForNextLevel = CalculateExperienceForLevel(level + 1);

        // Calculate progress percentage
        double expInCurrentLevel = experience - expForCurrentLevel;
        double expNeededForNextLevel = expForNextLevel - expForCurrentLevel;

        if (expNeededForNextLevel <= 0) return 100;

        double percentage = (expInCurrentLevel / expNeededForNextLevel) * 100;

        // Ensure percentage is within valid range
        if (percentage < 0) return 0;
        if (percentage > 100) return 100;

        return (int)percentage;
    }

    private static long CalculateExperienceForLevel(int level)
    {
        if (level <= 1) return 0;

        // Exp(Level) = (50 × (Level³ - Level)) / 3
        long levelCubed = (long)level * level * level;
        return (50 * (levelCubed - level)) / 3;
    }

    private static long CalculateExperienceToNextLevel(double experience, ushort level)
    {
        // Calculate experience needed for current level
        long expForCurrentLevel = CalculateExperienceForLevel(level);

        // If player has less experience than needed for current level,
        // treat them as level 1
        if (experience < expForCurrentLevel)
        {
            return CalculateExperienceForLevel(2) - (long)experience;
        }

        // Calculate experience needed for next level based on current level
        long expForNextLevel = CalculateExperienceForLevel(level + 1);

        // Calculate remaining experience needed
        long remaining = expForNextLevel - (long)experience;

        // If remaining is negative (player has more exp than needed), return 0
        return remaining > 0 ? remaining : 0;
    }

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
                VocationName = GetVocationName(entity.Vocation),
                RemainingRecoverySeconds = entity.RemainingRecoverySeconds,
                WorldId = entity.WorldId,
                HealthPercentage = CalculateHealthPercentage(entity.Health, entity.MaxHealth),
                ManaPercentage = CalculateManaPercentage(entity.Mana, entity.MaxMana),
                LevelPercentage = CalculateLevelPercentage(entity.Experience, entity.Level),
                Balance = entity.BankAmount,
                GroupName = GetGroupName(entity.Group),
                Sex = GetSexName(entity.Gender),
                WorldName = entity.World?.Name ?? "Unknown",
                TownName = entity.Town?.Name ?? "Unknown",
                DailyReward = "Open", // Mock
                Tutorial = "x", // Mock
                Blessings = "Access edit", // Mock
                ExperienceToNextLevel = CalculateExperienceToNextLevel(entity.Experience, entity.Level)
            };
    }
}