using Roguelike.Core.Abstractions.Behaviours;

namespace Roguelike.Core.Abstractions.Items;

public interface IInteractableItem : IItem
{
    void Interact(IPlayable playable);
}