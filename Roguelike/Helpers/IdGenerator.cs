namespace Roguelike.Helpers;

public class IdGenerator
{
    private static int counter;

    public static int GenerateId()
    {
        return ++counter;
    }
}