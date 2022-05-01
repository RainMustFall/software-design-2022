using Roguelike.Core.Abstractions.Map;
using Roguelike.Core.Abstractions.Items;

namespace Roguelike.Map.Cells
{
    public class ItemCell : ICell
    {
        public ItemCell(int x, int y, ItemType inventoryType)
        {
            X = x;
            Y = y;
            InventoryType = inventoryType;
        }
        public ItemType InventoryType { get; }
        public int X { get; }
        public int Y { get; }
        public char Render()
        {
            switch (InventoryType)
            {
                case ItemType.Body:
                    return 'B';
                case ItemType.Helmet:
                    return 'H';
                case ItemType.Weapon:
                    return 'W';
                default:
                    return ' ';
            }
        }
    }
}