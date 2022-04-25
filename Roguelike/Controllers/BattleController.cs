using Roguelike.Core.Abstractions.Behaviours;

namespace Roguelike.Controllers;

public class BattleController
{
    private readonly Random random = new();

    public void Battle(ICreature creatureFirst, ICreature creatureSecond)
    {
        HandleDamage(creatureFirst, creatureSecond.Properties.AttackPower);
        HandleDamage(creatureSecond, creatureFirst.Properties.AttackPower);
    }

    private void HandleDamage(ICreature creature, int damage)
    {
        if (true)
            creature.State.Confused.SetTrue(30);
        creature.State.ChangeCurrentHealth(damage * -1);
    }
}