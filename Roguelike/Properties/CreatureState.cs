using Roguelike.Helpers;

namespace Roguelike.Properties;

/// <summary>
/// Contains fields sufficient to describe current creature state, such as health, (mana, ...).
/// </summary>
public class CreatureState
{
    public CreatureState(int currentHealth)
    {
        CurrentHealth = currentHealth;
    }

    public int CurrentHealth { get; private set; }
    public TemporaryBoolean Confused { get; } = new();

    public void ChangeCurrentHealth(int delta)
    {
        CurrentHealth += delta;
    }
}