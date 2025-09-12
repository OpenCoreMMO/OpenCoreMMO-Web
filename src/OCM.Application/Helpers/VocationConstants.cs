namespace OCM.Application.Helpers;

public static class VocationConstants
{
    public static readonly List<VocationOption> AllVocations = new()
    {
        new VocationOption { Id = 1, Name = "Sorcerer", Description = "a sorcerer" },
        new VocationOption { Id = 2, Name = "Druid", Description = "a druid" },
        new VocationOption { Id = 3, Name = "Paladin", Description = "a paladin" },
        new VocationOption { Id = 4, Name = "Knight", Description = "a knight" },
        new VocationOption { Id = 5, Name = "Master Sorcerer", Description = "a master sorcerer" },
        new VocationOption { Id = 6, Name = "Elder Druid", Description = "an elder druid" },
        new VocationOption { Id = 7, Name = "Royal Paladin", Description = "a royal paladin" },
        new VocationOption { Id = 8, Name = "Elite Knight", Description = "an elite knight" },
        new VocationOption { Id = 9, Name = "Tutor", Description = "a tutor" },
        new VocationOption { Id = 10, Name = "Game Master", Description = "a Game Master" },
        new VocationOption { Id = 11, Name = "GOD", Description = "GOD" }
    };

    public static string GetVocationName(int vocationId)
    {
        var vocation = AllVocations.FirstOrDefault(v => v.Id == vocationId);
        return vocation?.Name ?? "Unknown";
    }

    public static VocationOption? GetVocation(int vocationId)
    {
        return AllVocations.FirstOrDefault(v => v.Id == vocationId);
    }
}

public class VocationOption
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}