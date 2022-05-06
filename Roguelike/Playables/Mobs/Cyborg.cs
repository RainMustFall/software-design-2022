using Roguelike.Core.Abstractions.Map;
using Roguelike.Map.Cells;
using Roguelike.Mobs;
using Roguelike.Mobs.Strategies;
using Roguelike.Properties;

namespace Roguelike.Playables.Mobs;

public class Cyborg : BaseMob
{
    public Cyborg(CompositeCell parentCell, IStrategy strategy)
        : base(
            parentCell,
            strategy,
            'C',
            new CreatureState(200),
            new CreatureProperties(200, 5)
        ) { }
}