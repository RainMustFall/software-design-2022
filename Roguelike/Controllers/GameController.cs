using Roguelike.Controllers.Misc;
using Roguelike.Controllers.Playables;
using Roguelike.Core;
using Roguelike.Core.Abstractions.Controllers;
using Roguelike.Map.Cells;
using Roguelike.Playables;

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

    private ControllerContainer GetControllerContainer()
    {
        return new ControllerContainer(MapController, InventoryEquipmentController);
    }
}