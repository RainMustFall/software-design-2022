namespace Roguelike.Properties;

public class CreatureState
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