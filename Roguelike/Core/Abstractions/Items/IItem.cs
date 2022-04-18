namespace Roguelike.Core.Abstractions.Items;

/// <summary>
/// Contains fields that are sufficient to describe any obtainable item. 
/// </summary>
public interface IItem
{
    string Name { get; }
}