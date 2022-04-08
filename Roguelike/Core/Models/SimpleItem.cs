using Roguelike.Core.Abstractions.Items;

namespace Roguelike.Core.Models;

public record SimpleItem(string Name) : IItem;