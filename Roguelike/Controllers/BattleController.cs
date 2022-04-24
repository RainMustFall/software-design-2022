using System;
using Roguelike.Core.Abstractions.Behaviours;

namespace Roguelike.Controllers;

public class BattleController
{
    public BattleController()
    {
        
    }
    
    public void Battle(ref ICreature сreatureFisrt, ref ICreature сreatureSecond)
    {
        сreatureSecond.State.ChangeCurrentHealth(сreatureFisrt.Properties.AttackPower * -1);
        сreatureFisrt.State.ChangeCurrentHealth(сreatureSecond.Properties.AttackPower * -1);
    }

    private void Attack()
    {
        
    }
}