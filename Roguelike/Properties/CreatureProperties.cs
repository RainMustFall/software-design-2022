namespace Roguelike.Properties;

/// <summary>
/// Contains properties sufficient to describe creature properties, such as maximum health, attack power, (attack speed, ...).
/// </summary>
public class CreatureProperties
{
    public int MaxHealth { get; private set; }
    public int AttackPower { get; private set; }
    public CreatureProperties(int maxHealth, int attackPower)
    {
        MaxHealth = maxHealth;
        AttackPower = attackPower;
    }
    public void SetMaxHealth(int newMaxHealth)
    {
        MaxHealth = newMaxHealth;
    }
};