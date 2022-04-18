namespace Roguelike.Helpers;

public class IdGenerator
{
    private static int Counter;

    public static int GenerateId()
    {
        return ++Counter;
    }
}