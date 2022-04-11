using Roguelike.Core.Abstractions.Items;

namespace Roguelike.Items;

public record SimpleItem(string Name) : IItem;