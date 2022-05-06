using Roguelike.Controllers;
using Roguelike.Map.Cells;
using Roguelike.Mobs;
using Roguelike.Mobs.Strategies;

namespace Roguelike.Core.Abstractions.Generation;

public abstract class MobFactory
{
    public abstract BaseMob CreateMob(Roguelike.Map.Map map);

    protected IStrategy PickRandomStrategy(Roguelike.Map.Map map)
    {
        var random = new Random();
        switch (random.Next(3))
        {
            case 0: return new AggressiveStrategy(map);
            case 1: return new CowardlyStrategy(map);
            case 2: return new RandomStrategy();
            default: throw new Exception("Random is broken!"); // impossible
        }
    }
}
