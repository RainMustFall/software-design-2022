using System;
using Roguelike.Controllers;
using Roguelike.Core.Abstractions.Items;
using Roguelike.Helpers;
using Roguelike.Items;
using Roguelike.Playables;
using Terminal.Gui;

namespace Roguelike.Core;

using Map = Map.Map;

/// <summary>
/// Main game class that is responsible for the creation of the game and processing of playable controller instances.
/// </summary>
public class Game
{
    private int counter;
    public readonly GameController GameController;
    public ProgressibleHumanoid MainCharacter { get; }

    private bool running = true;

    public Game()
    {
        // todo: make use of DI container and initialization (i.e. load a save)
        var map = new Map(200, 200);
        var mapController = new MapController(map);
        var inventoryEquipmentController = new InventoryEquipmentController();
        GameController = new GameController(this, mapController, inventoryEquipmentController);

        var playerCell = map.GetFirstEmptyCell();
        if (playerCell == null)
            throw new Exception("Generated map has no empty cells");
        MainCharacter = GameController.CreateHumanPlayer(playerCell);
        MainCharacter.Inventory.TryPutItem(new SimpleItem("Камень", ItemType.Weapon));
        MainCharacter.Inventory.TryPutItem(new SimpleItem("Японская удочка", ItemType.Weapon));
        MainCharacter.Inventory.TryPutItem(new SimpleItem("Сушёный кальмар", ItemType.Body));
        MainCharacter.Equipment.PutHelmetOn(new SimpleItem("Новогодняя шапка с оленями", ItemType.Helmet));
    }

    public void End()
    {
        running = false;
        Application.RequestStop();
    }

    public bool MakeIteration()
    {
        foreach (var playableController in GameController.PlayableControllers.Shuffle())
            playableController.Update();
        if (counter++ == 45)
            End();
        Application.Refresh();
        ShortcutHandler.UpdateState();
        return running;
    }
}