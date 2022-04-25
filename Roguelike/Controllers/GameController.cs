using Roguelike.Controllers.Misc;
using Roguelike.Controllers.Playables;
using Roguelike.Core;
using Roguelike.Core.Abstractions.Controllers;
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

    public GameController(Game game,
        MapController mapController,
        InventoryEquipmentController inventoryEquipmentController)
    {
        this.game = game;
        MapController = mapController;
        InventoryEquipmentController = inventoryEquipmentController;
    }

    public void EndGame()
    {
        game.End();
    }

    // todo: other factories and game methods...
    public ProgressibleHumanoid CreateHumanPlayer(CompositeCell initialPosition)
    {
        var cell = new PlayableCell(initialPosition);
        initialPosition.PutCell(cell);
        var humanoid = new ProgressibleHumanoid(cell);
        cell.Renderable = humanoid;
        var humanPlayerController = new HumanPlayerController(GetControllerContainer(), humanoid);
        PlayableControllers.Add(humanPlayerController);
        return humanoid;
    }

    public void SpawnMob(CompositeCell initialPosition)
    {
        var strategy = SelectStrategy();
        var dragon = new Dragon(initialPosition, strategy);
        var mobController = new MobController(GetControllerContainer(), dragon);
        initialPosition.PutCell(dragon.Cell);
        PlayableControllers.Add(mobController);
    }

    private IStrategy SelectStrategy()
    {
        var random = new Random();
        switch (random.Next(3))
        {
            case 0: return new AggressiveStrategy(MapController.Map);
            case 1: return new CowardlyStrategy(MapController.Map);
            case 2: return new RandomStrategy();
            default: throw new Exception("Random is broken!");  // impossible
        }
    }

    public void OnPlayableDeath(IPlayableController controller)
    {
        PlayableControllers.Remove(controller);
    }

    private ControllerContainer GetControllerContainer()
    {
        return new ControllerContainer(this, MapController, InventoryEquipmentController);
    }
}