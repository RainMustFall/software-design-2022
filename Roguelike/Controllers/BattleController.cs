using System;
using Roguelike.Core.Abstractions.Behaviours;

namespace Roguelike.Controllers;

public class BattleController
{
    public BattleController()
    {
    }

    public void Battle(ICreature сreatureFirst, ICreature сreatureSecond)
    {
        сreatureSecond.State.ChangeCurrentHealth(сreatureFirst.Properties.AttackPower * -1);
        сreatureFirst.State.ChangeCurrentHealth(сreatureSecond.Properties.AttackPower * -1);
    }
}