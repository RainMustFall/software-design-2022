using Roguelike.Map.Cells;

namespace Roguelike.Map;

public class Map
{
    public Map(int width, int height)
    {
        Cells = new CompositeCell[width, height];
        for (var i = 0; i < width; ++i)
        for (var j = 0; j < height; ++j)
            Cells[i, j] = new CompositeCell(i, j);
    }

    public CompositeCell[,] Cells { get; }
}