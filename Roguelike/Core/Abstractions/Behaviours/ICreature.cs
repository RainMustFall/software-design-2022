using Roguelike.Properties;

namespace Roguelike.Core.Abstractions.Behaviours;

/// <summary>
/// Contains fields and methods that are applicable to every playable creature.
/// </summary>
public interface ICreature : IPlayable
{
    CreatureState State { get; }
    CreatureProperties Properties { get; }
}