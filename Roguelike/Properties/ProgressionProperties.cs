namespace Roguelike.Properties;

/// <summary>
/// Stores information that is sufficient to describe current level, experience and skills.
/// </summary>
public struct ProgressionProperties
{
    public int Level { get; private set; }
    public int Experience { get; private set; }

    public ProgressionProperties(int level, int experience)
    {
        Level = level;
        Experience = experience;
    }

    public void NextLevel()
    {
        Level++;
    }

    public void IncreaseExperience(int delta = 1)
    {
        Experience += delta;
    }
}