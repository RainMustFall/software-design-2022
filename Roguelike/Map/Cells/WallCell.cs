using Roguelike.Core.Abstractions.Map;

namespace Roguelike.Map.Cells;

public class WallCell : ICell
{
    public WallCell(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; }
    public int Y { get; }

    public char Render()
    {
        return '%';
    }
}