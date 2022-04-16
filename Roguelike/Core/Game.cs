using Roguelike.Controllers;
using Terminal.Gui;

namespace Roguelike.Core;

using Map = Map.Map;

public class Game
{
    private readonly GameController gameController;
    public Map map { get; }

    public Game()
    {
        // todo: make use of DI container and initialization (i.e. load a save)
        map = new Map(100, 100);
        var mapController = new MapController(map);
        gameController = new GameController(mapController);
        
        var playerCell = map.GetFirstEmptyCell();
        if (playerCell == null)
            throw new Exception("Generated map has no empty cells");
        gameController.CreateHumanPlayer(playerCell);
    }

    public void Run()
    {
        while (true)
        {
            foreach (var playableController in gameController.PlayableControllers)
                playableController.Update();
            Application.Refresh();
        }
    }
}