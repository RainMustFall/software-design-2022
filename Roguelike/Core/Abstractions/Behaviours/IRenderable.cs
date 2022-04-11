using Roguelike.Core.Abstractions.Map;

namespace Roguelike.Core.Abstractions.Behaviours;

public interface IRenderable : IPlayable
{
    ICell Cell { get; }
}