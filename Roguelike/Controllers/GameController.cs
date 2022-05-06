using Roguelike.Controllers.Misc;
using Roguelike.Controllers.Playables;
using Roguelike.Core;
using Roguelike.Core.Abstractions.Controllers;
using Roguelike.Core.Abstractions.Generation;
using Roguelike.Map.Cells;
using Roguelike.Mobs;
using Roguelike.Mobs.Strategies;
using Roguelike.Playables;
using Roguelike.Playables.Mobs;

namespace Roguelike.Controllers;

/// <summary>
/// Main game controller class, that manipulates with instantiation of playable instances and disposal of those.
/// </summary>
public class GameController
{
    private readonly Game game;
    public readonly List<IPlayableController> PlayableControllers = new();
    public MapController MapController { get; }
    public InventoryEquipmentController InventoryEquipmentController { get; }
    public BattleController BattleController { get; }

    public GameController(Game game,
        MapController mapController,
        InventoryEquipmentController inventoryEquipmentController,
        BattleController battleController)
    {
        this.game = game;
        MapController = mapController;
        InventoryEquipmentController = inventoryEquipmentController;
        BattleController = battleController;
    }

    public void EndGame()
    {
        game.End();
    }

    // todo: other factories and game methods...
    public ProgressibleHumanoid CreateHumanPlayer(CompositeCell initialPosition)
    {
        var humanoid = new ProgressibleHumanoid(initialPosition);
        var humanPlayerController = new HumanPlayerController(GetControllerContainer(), humanoid);
        PlayableControllers.Add(humanPlayerController);
        return humanoid;
    }

    public void SpawnMob(Map.Map map)
    {
        var mobFactory = SelectFactory();
        var mob = mobFactory.CreateMob(map);
        var mobController = new MobController(GetControllerContainer(), mob);
        PlayableControllers.Add(mobController);
    }

    private MobFactory SelectFactory()
    {
        var random = new Random();
        switch (random.Next(2))
        {
            case 0: return new SciFiFactory();
            case 1: return new FantasyFactory();
            default: throw new Exception("Random is broken!"); // impossible
        }
    }
    
    public void SpawnFungus(CompositeCell initialPosition)
    {
        var fungus = new Fungus(initialPosition);
        var fungusController = new FungusController(GetControllerContainer(), fungus);
        PlayableControllers.Add(fungusController);
    }

    public void OnPlayableDeath(IPlayableController controller)
    {
        PlayableControllers.Remove(controller);
    }

    private ControllerContainer GetControllerContainer()
    {
        return new ControllerContainer(this, MapController, InventoryEquipmentController, BattleController);
    }
}