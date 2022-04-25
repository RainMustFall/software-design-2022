using System;
using FluentAssertions;
using NUnit.Framework;
using Roguelike.Controllers;
using Roguelike.Core.Abstractions.Behaviours;
using Roguelike.Core.Abstractions.Items;
using Roguelike.Core.Abstractions.Misc;
using Roguelike.Core.Abstractions.Map;
using Roguelike.Equipments;
using Roguelike.Inventories;
using Roguelike.Items;
using Roguelike.Properties;

namespace Roguelike_Tests.Controllers;

[TestFixture]
public class InventoryEquipmentController_Tests
{
    private InventoryEquipmentController controller;
    private IHumanoid humanoid;
    private IItem testItem;
    private IItem testItem2;

    [SetUp]
    public void TestSetup()
    {
        controller = new InventoryEquipmentController();
        humanoid = new TestHumanoid();
        testItem = new SimpleItem("Test", ItemType.Body);
        testItem2 = new SimpleItem("Test2", ItemType.Body);
    }

    [Test]
    public void Should_NotThrow_WhenNotEquipped()
    {
        var unwear = () => controller.UnwearBody(humanoid);
        unwear.Should().NotThrow();
        humanoid.Inventory.Should().BeEmpty();
        humanoid.Equipment.Helmet.Should().BeNull();
    }

    [Test]
    public void Should_StoreItemInInventory_WhenEquipped()
    {
        humanoid.Equipment.PutBodyOn(testItem);

        humanoid.Inventory.Should().NotContain(testItem);
        controller.UnwearBody(humanoid);
        humanoid.Inventory.Should().Contain(testItem);
        humanoid.Equipment.Body.Should().BeNull();
    }

    [Test]
    public void Synchronization_Inventory_And_Equipment_Test()
    {
        humanoid.Equipment.PutBodyOn(testItem);
        humanoid.Inventory.TryPutItem(testItem2);
        
        humanoid.Inventory.Should().NotContain(testItem);
        humanoid.Inventory.Should().Contain(testItem2);
        humanoid.Equipment.Body.Should().Be(testItem);
        
        controller.UnwearBody(humanoid);

        humanoid.Inventory.Should().Contain(testItem);
        humanoid.Inventory.Should().Contain(testItem2);
        humanoid.Equipment.Body.Should().BeNull();

        controller.PutBodyOn(humanoid, testItem2);
        
        humanoid.Inventory.Should().NotContain(testItem2);
        humanoid.Inventory.Should().Contain(testItem);
        humanoid.Equipment.Body.Should().Be(testItem2);
    }
    
    // TODO: Test checking case when inventory is about to be full

    private class TestHumanoid : IHumanoid
    {
        public CreatureState State { get; } = new CreatureState(100);
        public CreatureProperties Properties { get; } = new CreatureProperties(100, 10);
        public IInventory Inventory { get; } = new SimpleInventory();
        public IEquipment Equipment { get; } = new SimpleEquipment();
        public ICell Cell { get; }
    }
}