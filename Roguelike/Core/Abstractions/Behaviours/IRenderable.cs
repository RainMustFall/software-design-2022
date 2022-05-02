using Roguelike.Core.Abstractions.Map;

namespace Roguelike.Core.Abstractions.Behaviours;

/// <summary>
/// Contains fields that are sufficient to render a playable instance, i.e. arrows and characters.
/// </summary>
public interface IRenderable : IPlayable
{
    ICell Cell { get; }
}