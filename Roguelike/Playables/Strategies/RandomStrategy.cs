using Roguelike.Core.Abstractions.Map;
using Roguelike.Map.Cells;

namespace Roguelike.Mobs.Strategies;

public class RandomStrategy : IStrategy
{
    private Random random;

    public RandomStrategy()
    {
        random = new Random();
    }

    public (int, int) NextCoordinates(ICell cell)
    {
        return (cell.X + random.Next(-1, 2), cell.Y + random.Next(-1, 2));
    }
}