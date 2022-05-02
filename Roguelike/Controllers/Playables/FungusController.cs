using Roguelike.Controllers.Misc;
using Roguelike.Core.Abstractions.Controllers;
using Roguelike.Core.Abstractions.Map;
using Roguelike.Map.Cells;
using Roguelike.Playables;

namespace Roguelike.Controllers.Playables;

public class FungusController : IPlayableController
{
    private HashSet<Fungus> hashSet = new();
    private readonly ControllerContainer controllerContainer;
    private readonly float p;
    private readonly float q;
    private readonly Random random = new();

    public FungusController(ControllerContainer controllerContainer, Fungus baseFungus, float p = 0.2f, float q = 0.1f)
    {
        this.controllerContainer = controllerContainer;
        this.p = p;
        this.q = q;
        hashSet.Add(baseFungus);
    }

    public void Update()
    {
        var newHashSet = new HashSet<Fungus>();
        foreach (var fungus in hashSet)
        {
            if (ShouldDie())
                controllerContainer.MapController.RemoveCell(fungus.Cell);
            else
                newHashSet.Add(fungus);

            var cell = GetEmptyCellNearby(fungus.Cell);
            if (!ShouldReplicate() || cell == null)
                continue;
            var newFungus = fungus.Clone();
            if (controllerContainer.MapController.Move(newFungus.Cell, cell.X, cell.Y))
                (newFungus.Cell as MobCell)!.ParentCell = cell;
            newHashSet.Add(newFungus);
        }

        hashSet = newHashSet;
    }

    public void OnDeath()
    {
        foreach (var fungus in hashSet)
            controllerContainer.MapController.RemoveCell(fungus.Cell);
    }

    public void OnTriggerRenderingCreature(ICell cell)
    {
    }

    private bool ShouldReplicate()
    {
        return random.NextDouble() < p;
    }

    private bool ShouldDie()
    {
        return random.NextDouble() < q;
    }

    private CompositeCell? GetEmptyCellNearby(ICell cell)
    {
        for (var i = -1; i <= 1; ++i)
        for (var j = -1; j <= 1; ++j)
            if (controllerContainer.MapController.GetCell(cell.X + i, cell.Y + j)?.Empty() ?? false)
                return controllerContainer.MapController.GetCell(cell.X + i, cell.Y + j);
        return null;
    }
}