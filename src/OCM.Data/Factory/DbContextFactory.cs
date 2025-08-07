namespace OCM.Infrastructure.Factory;

public class DbContextFactory
{
    private static DbContextFactory _instance;

    public static DbContextFactory GetInstance()
    {
        return _instance ??= new DbContextFactory();
    }
}