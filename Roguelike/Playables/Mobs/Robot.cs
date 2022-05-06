using Roguelike.Core.Abstractions.Map;
using Roguelike.Map.Cells;
using Roguelike.Mobs;
using Roguelike.Mobs.Strategies;
using Roguelike.Properties;

namespace Roguelike.Playables.Mobs;

public class Robot : BaseMob
{
    public Robot(CompositeCell parentCell, IStrategy strategy)
        : base(
            parentCell,
            strategy,
            'R',
            new CreatureState(200),
            new CreatureProperties(200, 5)
        ) { }
}