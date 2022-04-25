using Roguelike.Core.Abstractions.Behaviours;
using Roguelike.Core.Abstractions.Map;

namespace Roguelike.Map.Cells;

public class MobCell : ICell
{
    private char image;
    public CompositeCell ParentCell { get; set; }
    public MobCell(CompositeCell parentCell, char image)
    {
        ParentCell = parentCell;
        this.image = image;
    }
    
    public int X => ParentCell.X;
    public int Y => ParentCell.Y;
    
    public char Render()
    {
        return image;
    }
}