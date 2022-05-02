using Roguelike.Core.Abstractions.Map;
using Roguelike.Map.Cells;

namespace Roguelike.Mobs.Strategies;

/// <summary>
/// Runs away from player.
/// </summary>
public class CowardlyStrategy : IStrategy
{
    private Map.Map map;
    private int radius;

    public CowardlyStrategy(Map.Map map, int radius = 5)
    {
        this.map = map;
        this.radius = radius;
    }

    public (int, int) NextCoordinates(ICell cell)
    {
        var player = map.LocatePlayer();
        if (player == null)
            return (cell.X, cell.Y);

        var dx = player.X - cell.X;
        var dy = player.Y - cell.Y;

        if (Math.Abs(dx) > radius || Math.Abs(dy) > radius)
            return (cell.X, cell.Y);

        if (dy <= dx && dy > -dx)
            return (cell.X + (dx > 0 ? -1 : 1), cell.Y);

        return (cell.X, cell.Y + (dy > 0 ? -1 : 1));
    }
}