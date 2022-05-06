using Roguelike.Map.Cells;
using Roguelike.Mobs;
using Roguelike.Playables.Mobs;

namespace Roguelike.Core.Abstractions.Generation;

public class SciFiFactory : MobFactory
{
    private Random random;
    
    public SciFiFactory()
    {
        random = new Random();
    }

    public override BaseMob CreateMob(Roguelike.Map.Map map)
    {
        switch (random.Next(2))
        {
            case 0: return new Robot(map.GetRandomEmptyCell()!, PickRandomStrategy(map));
            case 1: return new Cyborg(map.GetRandomEmptyCell()!, PickRandomStrategy(map));
            default: throw new Exception("Random is broken!"); // impossible
        }
    }
}