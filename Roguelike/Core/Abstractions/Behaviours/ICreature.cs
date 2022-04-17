using Roguelike.Properties;

namespace Roguelike.Core.Abstractions.Behaviours;

public interface ICreature : IPlayable
{
    CreatureState State { get; }
    CreatureProperties Properties { get; }
}