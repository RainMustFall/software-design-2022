namespace Roguelike.Helpers;

public static class LinqExtensions
{
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
    {
        var random = new Random();
        return enumerable.OrderBy(x => random.Next());
    }
}