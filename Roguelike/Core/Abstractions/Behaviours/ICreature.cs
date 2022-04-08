using Roguelike.Core.Models;

namespace Roguelike.Core.Abstractions.Behaviours;

public interface ICreature : IPlayable
{
    CreatureState State { get; }
    CreatureProperties Properties { get; }
}