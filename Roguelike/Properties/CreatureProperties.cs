namespace Roguelike.Properties;

public class CreatureProperties
{
    public int MaxHealth { get; private set; }
    public int AttackPower { get; private set; }
    public CreatureProperties(int maxHealth, int attackPower)
    {
        MaxHealth = maxHealth;
        AttackPower = attackPower;
    }

    public void ChangeHealth(int delta)
    {
        MaxHealth += delta;
    }

    public void ChangeAttackPower(int delta)
    {
        AttackPower += delta;
    }
};