using Roguelike.Mobs;
using Roguelike.Mobs.Strategies;
using Roguelike.Playables.Mobs;

namespace Roguelike.Core.Abstractions.Generation;

public class FantasyFactory : MobFactory
{
    private Random random;
    
    public FantasyFactory()
    {
        random = new Random();
    }

    public override BaseMob CreateMob(Roguelike.Map.Map map)
    {
        switch (random.Next(2))
        {
            case 0: return new Skeleton(map.GetRandomEmptyCell()!, PickRandomStrategy(map));
            case 1: return new Dragon(map.GetRandomEmptyCell()!, PickRandomStrategy(map));
            default: throw new Exception("Random is broken!"); // impossible
        }
    }
}