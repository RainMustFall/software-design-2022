using Roguelike.Core.Abstractions.Map;
using Roguelike.Map.Cells;

namespace Roguelike.Mobs.Strategies;

/// <summary>
/// Chases player if it is seen in some radius.
/// </summary>
public class AggressiveStrategy : IStrategy
{
    private Map.Map map;
    private int radius;
    private bool[,] used;

    public AggressiveStrategy(Map.Map map, int radius = 15)
    {
        this.map = map;
        this.radius = radius;
        used = new bool[2 * radius + 1, 2 * radius + 1];
    }

    public (int, int) NextCoordinates(ICell cell)
    {
        var player = map.LocatePlayer();
        if (player == null)
            return (0, 0);

        var dx = player.X - cell.X;
        var dy = player.Y - cell.Y;

        if (Math.Abs(dx) > radius || Math.Abs(dy) > radius)
            return (cell.X, cell.Y);

        return PerformBfs(player, cell);
    }

    private (int, int) PerformBfs(ICell start, ICell finish)
    {
        ClearRoute();
        var queue = new Queue<ICell>();
        queue.Enqueue(start);
        used[radius, radius] = true;
        while (queue.Count != 0)
        {
            var current = queue.Dequeue();
            for (var dx = -1; dx <= 1; ++dx)
            for (var dy = -1; dy <= 1; ++dy)
                if (Legal(current.X + dx, current.Y + dy, start, finish))
                {
                    var next = map.Cells[current.X + dx, current.Y + dy];
                    if (next.X == finish.X && next.Y == finish.Y)
                        return (current.X, current.Y);

                    queue.Enqueue(next);
                    used[next.X - start.X + radius, next.Y - start.Y + radius] = true;
                }
        }

        return (finish.X, finish.Y);
    }

    private bool BetweenBounds(int idx, Array array, int dimension)
    {
        return idx >= array.GetLowerBound(dimension) && idx <= array.GetUpperBound(dimension);
    }

    private bool Legal(int x, int y, ICell start, ICell finish)
    {
        return
            // Inside map
            BetweenBounds(x, map.Cells, 0)
            && BetweenBounds(y, map.Cells, 1)
            // inside radius-neighborhood
            && BetweenBounds(x - start.X + radius, used, 0)
            && BetweenBounds(y - start.Y + radius, used, 1)
            // empty and not visited before
            && (map.Cells[x, y].Empty() || x == finish.X && y == finish.Y)
            && !used[x - start.X + radius, y - start.Y + radius];
    }

    private void ClearRoute()
    {
        for (var i = 0; i < 2 * radius + 1; i++)
        for (var j = 0; j < 2 * radius + 1; j++)
            used[i, j] = false;
    }
}