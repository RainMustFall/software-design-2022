namespace Roguelike.Properties;

/// <summary>
/// Contains fields sufficient to describe current creature state, such as health, (mana, ...).
/// </summary>
public class CreatureState
{
    public int CurrentHealth { get; set; }

    public CreatureState(int currentHealth)
    {
        CurrentHealth = currentHealth;
    }

    public void ChangeCurrentHealth(int delta)
    {
        CurrentHealth += delta;
    }
}