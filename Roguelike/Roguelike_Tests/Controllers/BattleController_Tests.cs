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
public class BattleController_Tests
{
    private BattleController battleController;
    private ICreature сreatureFisrt;
    private ICreature сreatureSecond;

    [SetUp]
    public void TestSetup()
    {
        battleController = new BattleController();
        сreatureFisrt = new TestBattleHumanoid(100, 20);
        сreatureSecond = new TestBattleHumanoid(120, 10);
    }

    [Test]
    public void SingleAttack_Test()
    {
        battleController.Battle(ref сreatureFisrt, ref сreatureSecond);
        сreatureFisrt.State.CurrentHealth.Should().Be(90);
        сreatureSecond.State.CurrentHealth.Should().Be(100);
    }
 
    private class TestBattleHumanoid : IHumanoid
    {
        public TestBattleHumanoid(int health, int attackPower)
        {
            State = new CreatureState(health);
            Properties = new CreatureProperties(health, attackPower);
        }
        public CreatureState State { get; }
        public CreatureProperties Properties { get; }
        public IInventory Inventory { get; } = new SimpleInventory();
        public IEquipment Equipment { get; } = new SimpleEquipment();
    }
}
