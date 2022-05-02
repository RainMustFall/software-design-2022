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
public class BattleController_Tests
{
    private BattleController battleController;
    private ICreature? сreatureFisrt;
    private ICreature? сreatureSecond;

    [SetUp]
    public void TestSetup()
    {
        battleController = new BattleController();
        сreatureFisrt = new TestHumanoid(100, 20);
        сreatureSecond = new TestHumanoid(120, 10);
    }

    [Test]
    public void SingleAttack_Test()
    {
        battleController.Battle(сreatureFisrt, сreatureSecond);
        сreatureFisrt?.State.CurrentHealth.Should().Be(90);
        сreatureSecond?.State.CurrentHealth.Should().Be(100);
    }

    [Test]
    public void DeathCreature_Test()
    {
        // six attack for death second сreature 
        for (var i = 0; i < 6; i++)
        {
            battleController.Battle(сreatureFisrt, сreatureSecond);
        }
        сreatureFisrt?.State.CurrentHealth.Should().Be(40);
        сreatureSecond?.State.CurrentHealth.Should().Be(0);
    }
}