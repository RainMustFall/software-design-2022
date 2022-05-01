namespace Roguelike.Properties;

/// <summary>
/// Stores information that is sufficient to describe current level, experience and skills.
/// </summary>
public class ProgressionProperties
{
    public int Level { get; private set; }
    public int Experience { get; private set; }
    public int MaxLevel { get; }
    public int MaxExperience { get; }
    
    public ProgressionProperties(int level, int experience, int maxExperience = 3, int maxLevel = 5)
    {
        Level = level;
        Experience = experience;
        MaxExperience = maxExperience;
        MaxLevel = maxLevel;
    }

    /// <summary>
    /// Move to next game level.
    /// </summary>
    public void NextLevel()
    {
        Level++;
    }

    /// <summary>
    /// Increase experience after killing a mob.
    /// </summary>
    public void IncreaseExperience()
    {
        Experience++;
        if (Experience >= MaxExperience)
        {
            Experience = 0;
            NextLevel();
        }
    }
}