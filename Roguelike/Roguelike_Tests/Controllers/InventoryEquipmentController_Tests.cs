using System;
using FluentAssertions;
using NUnit.Framework;
using Roguelike.Controllers;
using Roguelike.Core.Abstractions.Behaviours;
using Roguelike.Core.Abstractions.Items;
using Roguelike.Core.Abstractions.Misc;
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

    [SetUp]
    public void TestSetup()
    {
        controller = new InventoryEquipmentController();
        humanoid = new TestHumanoid();
        testItem = new SimpleItem("Test");
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

    // TODO: Test checking case when inventory is about to be full

    private class TestHumanoid : IHumanoid
    {
        public CreatureState State { get; } = new();
        public CreatureProperties Properties { get; } = new();
        public IInventory Inventory { get; } = new SimpleInventory();
        public IEquipment Equipment { get; } = new SimpleEquipment();
    }
}