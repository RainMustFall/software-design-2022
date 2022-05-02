using Roguelike.Core.Abstractions.Behaviours;
using Roguelike.Core.Abstractions.Map;
using Roguelike.Mobs.Strategies;

namespace Roguelike.Controllers.BaseControllers;

// little dirty hack just to see internals of base playable controller
public abstract partial class BasePlayableController
{
    /// <summary>
    /// Options of <see cref="BasePlayableController"/> which allows user to toggle on/off required playable features,
    /// such as death handling, confusion handling etc...
    /// </summary>
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

        #region Handlers

        public BehaviourOptions WithDeathHandling(ICreature creature)
        {
            AddAction(controller =>
            {
                if (creature.State.CurrentHealth <= 0)
                    controller.OnDeath();
            });
            return this;
        }

        public BehaviourOptions WithConfusionHandling(IRenderingCreature creature)
        {
            AddAction(controller =>
            {
                if (!creature.State.Confused)
                    return;

                controller.shouldSkipUpdate.SetTrue(1);
                var (newX, newY) = new RandomStrategy().NextCoordinates(creature.Cell);
                if (controller.MapController.Move(creature.Cell, newX, newY) &&
                    creature.Cell is IPlayableCell playableCell)
                    playableCell.ParentCell = controller.MapController.Map.Cells[newX, newY];
            });
            return this;
        }

        #endregion

    }
}