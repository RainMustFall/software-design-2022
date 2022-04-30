using Roguelike.Core.Abstractions.Behaviours;
using Roguelike.Core.Abstractions.Items;
using Roguelike.Core.Abstractions.Misc;
using Roguelike.Core.Abstractions.Map;
using Roguelike.Properties;
using Roguelike.Equipments;
using Roguelike.Inventories;
using Roguelike.Items;

namespace Roguelike_Tests.Controllers;

internal class TestCell : ICell
{
    public TestCell(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; }
    public int Y { get; }
    public char Render()
    {
        throw new System.NotImplementedException();
    }
}

internal class TestHumanoid : IHumanoid
{
    public TestHumanoid(int health, int attackPower)
    {
        State = new CreatureState(health);
        Properties = new CreatureProperties(health, attackPower);
        Cell = new TestCell(1,1);
    }
    public TestHumanoid(int health, int attackPower, ICell cell)
    {
        State = new CreatureState(health);
        Properties = new CreatureProperties(health, attackPower);
        Cell = cell;
    }
    public CreatureState State { get; }
    public CreatureProperties Properties { get; }
    public IInventory Inventory { get; } = new SimpleInventory();
    public IEquipment Equipment { get; } = new SimpleEquipment();
    public ICell Cell { get; }
}