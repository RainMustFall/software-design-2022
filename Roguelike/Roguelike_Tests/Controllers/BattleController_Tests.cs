﻿using System;
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
public class BattleController_Tests
{
    private BattleController battleController;
    private IRenderingCreature? сreatureFisrt;
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
        battleController.Battle(сreatureFisrt, сreatureSecond);
        сreatureFisrt.State.CurrentHealth.Should().Be(90);
        сreatureSecond.State.CurrentHealth.Should().Be(100);
    }

    [Test]
    public void DeathCreature_Test()
    {
        // six attack for death second сreature 
        for (var i = 0; i < 6; i++)
        {
            battleController.Battle(сreatureFisrt, сreatureSecond);
        }

        сreatureFisrt.State.CurrentHealth.Should().Be(40);
        сreatureSecond.State.CurrentHealth.Should().Be(0);
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
        public ICell Cell { get; }
    }
}