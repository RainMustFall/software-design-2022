namespace Roguelike.Properties;

public class ProgressionProperties
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