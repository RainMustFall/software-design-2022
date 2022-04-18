using Roguelike.Controllers;
using Roguelike.Items;
using Roguelike.Playables;
using Terminal.Gui;

namespace Roguelike.Core;

using Map = Map.Map;

public class Game
{
    private readonly GameController gameController;
    public Map map { get; }
    public ProgressibleHumanoid character { get; }

    public Game()
    {
        // todo: make use of DI container and initialization (i.e. load a save)
        map = new Map(200, 200);
        var mapController = new MapController(map);
        gameController = new GameController(mapController);

        var playerCell = map.GetFirstEmptyCell();
        if (playerCell == null)
            throw new Exception("Generated map has no empty cells");
        character = gameController.CreateHumanPlayer(playerCell);
        character.Inventory.TryPutItem(new SimpleItem("Камень"));
        character.Inventory.TryPutItem(new SimpleItem("Японская удочка"));
        character.Inventory.TryPutItem(new SimpleItem("Сушёный кальмар"));
        character.Equipment.PutHelmetOn(new SimpleItem("Новогодняя шапка с оленями"));
    }

    public void MakeIteration()
    {
        foreach (var playableController in gameController.PlayableControllers)
            playableController.Update();
        Application.Refresh();
    }
}