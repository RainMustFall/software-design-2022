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
    
    public ProgressionProperties(int level, int experience, int maxExperience = 2, int maxLevel = 10)
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
        RaiseMoveToNextLevel();
    }

    /// <summary>
    /// Increase experience after killing a mob.
    /// </summary>
    public void IncreaseExperience()
    {
        Experience++;
        if (Experience >= MaxExperience)
            NextLevel();
    }
    public void RefreshExperience()
    {
        Experience = 0;
    }
    public delegate void MoveToNextLevelHandler(object sender, MoveToNextLevelArgs e);
    public event MoveToNextLevelHandler MoveToNextLevel;
    private void RaiseMoveToNextLevel()
    {
        MoveToNextLevel?.Invoke(this, new MoveToNextLevelArgs(Level));
    }
}

public class MoveToNextLevelArgs
{
    public MoveToNextLevelArgs(int level) { Level = level; }
    public int Level { get; }
}