using Roguelike.Core.Abstractions.Behaviours;

namespace Roguelike.Controllers.BaseControllers;

public class BehaviourOptions
{
    private readonly List<Action<BasePlayableController>> additionalActions;
    private BehaviourOptions(List<Action<BasePlayableController>>? additionalActions = null)
    {
        this.additionalActions = additionalActions ?? new List<Action<BasePlayableController>>();
    }

    public static BehaviourOptions New()
    {
        return new BehaviourOptions();
    }

    public void AddAction(Action<BasePlayableController> action)
    {
        additionalActions.Add(action);
    }

    public IEnumerable<Action<BasePlayableController>> OnUpdateActions()
    {
        return additionalActions;
    }
}

public static class BehaviourOptionsExtensions
{
    public static BehaviourOptions WithDeathHandling(this BehaviourOptions behaviourOptions, ICreature creature)
    {
        behaviourOptions.AddAction(controller =>
        {
            if (creature.State.CurrentHealth <= 0)
                controller.OnDeath();
        });
        return behaviourOptions;
    }
}