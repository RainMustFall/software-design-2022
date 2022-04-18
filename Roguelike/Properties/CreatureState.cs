namespace Roguelike.Properties;

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