namespace Roguelike.Helpers;

public class TemporaryBoolean
{
    private int remaining;

    public static implicit operator bool(TemporaryBoolean boolean)
    {
        if (boolean.remaining == 0)
            return false;

        boolean.remaining--;
        return true;
    }

    public void SetTrue(int requestsAmount)
    {
        remaining += Math.Max(0, requestsAmount);
    }
}