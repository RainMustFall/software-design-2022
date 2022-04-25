using Roguelike.Core.Abstractions.Behaviours;
using Roguelike.Core.Abstractions.Map;
using Roguelike.Map.Cells;
using Roguelike.Properties;

namespace Roguelike.Mobs;

public class BaseMob :  IRenderingCreature //ICreature, IRenderable
{
    public CreatureState State { get; }
    public CreatureProperties Properties { get; }
    public ICell Cell { get; }
    public IStrategy MovementStrategy { get; }

    public BaseMob(CompositeCell parentCell, IStrategy movementStrategy,
        char image, CreatureState state, CreatureProperties properties)
    {
        Cell = new MobCell(parentCell, image);
        State = state;
        Properties = properties;
        MovementStrategy = movementStrategy;
    }
}