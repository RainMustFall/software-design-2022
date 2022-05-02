using Roguelike.Core.Abstractions.Items;

namespace Roguelike.Items;

public class SimpleItem : IItem
{
    public string Name { get; }
    public ItemType Type { get; }
    public SimpleItem(string name, ItemType type)
    {
        Name = name;
        Type = type;
    }
    public SimpleItem(ItemType type)
    {
        Type = type;
        Name = Type switch
        {
            ItemType.Body => "Бронежилет",
            ItemType.Helmet => "Каска",
            ItemType.Weapon => "Камень",
            _ => Name
        };
    }
}