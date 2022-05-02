namespace Roguelike.Core.Abstractions.Behaviours;

/// <summary>
/// An abstraction for 'Prototype' pattern, which allows users to clone objects safely.
/// </summary>
public interface ICloneable<out TBase>
{
    TBase Clone();
}