using Roguelike.Controllers.BaseControllers;
using Roguelike.Controllers.Misc;
using Roguelike.Core.Abstractions.Map;
using Roguelike.Helpers;
using Roguelike.Map.Cells;
using Roguelike.Playables;
using SharpHook.Native;

namespace Roguelike.Controllers.Playables;

/// <summary>
/// Controller of player humanoid.
/// </summary>
public class HumanPlayerController : BasePlayableController
{
    private readonly ProgressibleHumanoid player;

    public HumanPlayerController(ControllerContainer controllerContainer,
        ProgressibleHumanoid player) : base(controllerContainer, player,
        BehaviourOptions.New().WithDeathHandling(player))
    {
        this.player = player;
    }

    protected override void UpdateInner()
    {
        var deltaX = 0;
        var deltaY = 0;

        if (ShortcutHandler.IsPressed(KeyCode.VcW))
            deltaY += -1;
        if (ShortcutHandler.IsPressed(KeyCode.VcS))
            deltaY += 1;
        if (ShortcutHandler.IsPressed(KeyCode.VcA))
            deltaX += -1;
        if (ShortcutHandler.IsPressed(KeyCode.VcD))
            deltaX += 1;

        if (deltaX != 0 || deltaY != 0)
        {
            var newX = player.Cell.X + deltaX;
            var newY = player.Cell.Y + deltaY;
            var newPlayerCell = MapController.Map.Cells[newX, newY];
            if (newPlayerCell.ContainsMob())
                OnTriggerRenderingCreature(newPlayerCell);
            if (newPlayerCell.ContainsItem())
                OnTriggerInventory(newPlayerCell);
            if (MapController.Move(player.Cell, newX, newY))
                (player.Cell as PlayableCell)!.ParentCell = MapController.Map.Cells[newX, newY];
        }
    }

    private void OnTriggerInventory(ICell cell)
    {
        if (cell is CompositeCell compositeCell)
        {
            player.Inventory.TryPutItem(compositeCell.GetItemFromCell());
            MapController.Map.Cells[cell.X, cell.Y].RemoveItemFromCell();
        }
    }

    public override void OnTriggerRenderingCreature(ICell cell)
    {
        if (cell is CompositeCell compositeCell)
        {
            BattleController.Battle(player, compositeCell.GetCreatureInCell());
        }
    }

    protected override void OnDeathInner()
    {
        Task.Run(async () =>
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            GameController.EndGame();
            Console.WriteLine("You died :(");
        });
    }
}