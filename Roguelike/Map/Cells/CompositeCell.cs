using Roguelike.Core.Abstractions.Map;

namespace Roguelike.Map.Cells;

public class CompositeCell : ICell
{
    public CompositeCell(int x, int y, params ICell[] cells)
    {
        innerCells = cells.ToList();
        X = x;
        Y = y;
    }

    private readonly List<ICell> innerCells;
    public int X { get; }
    public int Y { get; }

    public char Render()
    {
        return innerCells.Count > 0 ? innerCells.First().Render() : '.';
    }

    public void PutCell(ICell cell)
    {
        // reorder underlying cells if you care about rendering priority
        innerCells.Add(cell);
    }

    public void RemoveCell(ICell cell)
    {
        innerCells.Remove(cell);
    }

    public bool Empty()
    {
        return innerCells.Count == 0;
    }
    
    
    public bool IsPlayer()
    {
        return innerCells.FindIndex(cell => cell is PlayableCell) != -1;
    }
}