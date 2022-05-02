using Roguelike.Core.Abstractions.Behaviours;
using Roguelike.Map.Cells;

namespace Roguelike.Core.Abstractions.Map;

public interface IPlayableCell : ICell
{
    public CompositeCell ParentCell { get; set; }
    public IRenderable Renderable { get; }
}