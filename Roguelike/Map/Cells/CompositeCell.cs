using Roguelike.Core.Abstractions.Map;
using Roguelike.Core.Abstractions.Behaviours;

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

    public ICreature GetCreatureInCell()
    {
        if (ContainsPlayable() && innerCells.Find(cell => cell is IPlayableCell) is IPlayableCell { Renderable: ICreature creature })
            return creature;

        throw new Exception("Creature is not present in cell. This is surely a bug in code.");
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

    public bool ContainsPlayer()
    {
        return innerCells.FindIndex(cell => cell is PlayableCell) != -1;
    }
    
    public bool ContainsPlayable()
    {
        return innerCells.FindIndex(cell => cell is IPlayableCell) != -1;
    }
}