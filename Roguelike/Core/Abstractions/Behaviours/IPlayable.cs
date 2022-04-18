using Roguelike.Helpers;

namespace Roguelike.Core.Abstractions.Behaviours;

public interface IPlayable
{
    public int Id => IdGenerator.GenerateId();
}