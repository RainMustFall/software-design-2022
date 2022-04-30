using Roguelike.Controllers.Misc;
using Roguelike.Core.Abstractions.Behaviours;
using Roguelike.Core.Abstractions.Controllers;
using Roguelike.Core.Abstractions.Map;
using Roguelike.Helpers;

namespace Roguelike.Controllers.BaseControllers;

/// <summary>
/// Base class that contains convenient methods and fields that might be useful for many IPlayableController instances.
/// </summary>
public abstract partial class BasePlayableController : IPlayableController
{
    private readonly ControllerContainer controllerContainer;
    private readonly BehaviourOptions options;
    private readonly IPlayable? playable;
    private readonly TemporaryBoolean shouldSkipUpdate = new();
    private bool dead;

    protected BasePlayableController(ControllerContainer controllerContainer,
        IPlayable? playable,
        BehaviourOptions? options = null)
    {
        this.controllerContainer = controllerContainer;
        this.playable = playable;
        this.options = options ?? BehaviourOptions.New();
    }

    protected GameController GameController => controllerContainer.GameController;

    protected MapController MapController => controllerContainer.MapController;

    protected InventoryEquipmentController InventoryEquipmentController =>
        controllerContainer.InventoryEquipmentController;
    
    protected BattleController BattleController => controllerContainer.BattleController;  

    public void Update()
    {
        foreach (var action in options.OnUpdateActions())
            action(this);
        if (dead || shouldSkipUpdate)
            return;
        UpdateInner();
    }

    public void OnDeath()
    {
        dead = true;
        OnDeathInner();
        GameController.OnPlayableDeath(this);
        if (playable is IRenderable renderable)
            MapController.RemoveCell(renderable.Cell);
    }

    public abstract void OnTrigger(ICell cell);

    protected abstract void UpdateInner();

    protected abstract void OnDeathInner();
}