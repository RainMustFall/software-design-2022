using Roguelike.Controllers;
using Terminal.Gui;

namespace Roguelike.Core;

public class Game
{
    private readonly GameController gameController;

    public Game()
    {
        // todo: make use of DI container and initialization (i.e. load a save)
        var map = new Map.Map(10, 10);
        var mapController = new MapController(map);
        gameController = new GameController(mapController);
        gameController.CreateHumanPlayer(5, 5);
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