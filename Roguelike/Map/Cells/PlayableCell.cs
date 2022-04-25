using Roguelike.Core.Abstractions.Behaviours;
using Roguelike.Core.Abstractions.Map;

namespace Roguelike.Map.Cells;

public class PlayableCell : IPlayableCell
{
    public CompositeCell ParentCell { get; set; }
    public IRenderable Renderable { get; }

    public PlayableCell(CompositeCell parentCell, IRenderable renderable)
    {
        ParentCell = parentCell;
        parentCell.PutCell(this);
        Renderable = renderable;
    }

    public int X => ParentCell.X;
    public int Y => ParentCell.Y;

    public char Render()
    {
        return '@';
    }
}