namespace Roguelike.Helpers;

internal class IdGenerator
{
    private static int counter;

    public static int GenerateId()
    {
        return ++counter;
    }
}