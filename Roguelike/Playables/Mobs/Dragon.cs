using Roguelike.Core.Abstractions.Map;
using Roguelike.Map.Cells;
using Roguelike.Mobs;
using Roguelike.Mobs.Strategies;
using Roguelike.Properties;

namespace Roguelike.Playables.Mobs;

public class Dragon : BaseMob
{
    public Dragon(CompositeCell parentCell, IStrategy strategy)
        : base(
            parentCell,
            strategy,
            'D',
            new CreatureState(100),
            new CreatureProperties(100, 50)
        )
    {
    }
}