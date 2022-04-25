using Roguelike.Core.Abstractions.Behaviours;
using Roguelike.Core.Abstractions.Map;

namespace Roguelike.Map.Cells;

public class MobCell : IPlayableCell
{
    private char image;
    public CompositeCell ParentCell { get; set; }
    public IRenderable Renderable { get; }
    public MobCell(CompositeCell parentCell, char image, IRenderable renderable)
    {
        ParentCell = parentCell;
        this.image = image;
        Renderable = renderable;
        parentCell.PutCell(this);
    }

    public int X => ParentCell.X;
    public int Y => ParentCell.Y;

    public char Render()
    {
        return image;
    }
}