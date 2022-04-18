namespace Roguelike.Properties;

/// <summary>
/// Contains fields sufficient to describe current creature state, such as health, (mana, ...).
/// </summary>
public struct CreatureState
{
    public int CurrentHealth { get; private set; }

    public CreatureState(int currentHealth)
    {
        CurrentHealth = currentHealth;
    }

    public void ChangeCurrentHealth(int delta)
    {
        CurrentHealth += delta;
    }
}