using Roguelike.Core.Abstractions.Behaviours;
using Roguelike.Core.Abstractions.Map;
using Roguelike.Map.Cells;
using Roguelike.Properties;

namespace Roguelike.Playables;

public class Fungus : IRenderable, ICreature, ICloneable<Fungus>
{
    private readonly MobCell cell;

    public Fungus(CompositeCell initialPosition)
    {
        cell = new MobCell(initialPosition, 'F', this);
    }

    public ICell Cell => cell;
    public CreatureState State { get; } = new(1);
    public CreatureProperties Properties { get; } = new(1, 1);

    public Fungus Clone()
    {
        return new Fungus(cell.ParentCell);
    }
}