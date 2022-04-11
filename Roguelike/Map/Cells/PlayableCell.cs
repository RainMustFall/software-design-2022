using Roguelike.Core.Abstractions.Map;

namespace Roguelike.Map.Cells;

public class PlayableCell : ICell
{
    public CompositeCell ParentCell { get; set; }

    public PlayableCell(CompositeCell parentCell)
    {
        ParentCell = parentCell;
    }

    public int X => ParentCell.X;
    public int Y => ParentCell.Y;

    public char Render()
    {
        return '@';
    }
}