using Roguelike.Core.Abstractions.Map;
using Roguelike.Map.Cells;
using Roguelike.Mobs;
using Roguelike.Mobs.Strategies;
using Roguelike.Properties;

namespace Roguelike.Playables.Mobs;

public class Skeleton : BaseMob
{
    public Skeleton(CompositeCell parentCell, IStrategy strategy)
        : base(
            parentCell,
            strategy,
            'S',
            new CreatureState(50),
            new CreatureProperties(50, 5)
        ) { }
}