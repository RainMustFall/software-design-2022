namespace Roguelike.Map;

/// <summary>
/// MapBuilder interface. 
/// </summary>
public interface IMapBuilder
{
    /// <summary>
    /// Create map.
    /// </summary>
    /// <returns></returns>
    Map Build();
}

/// <summary>
/// Builder for create map. 
/// </summary>
public class MapBuilder : IMapBuilder
{
    private readonly bool generateMap;
    private int mapWidth;
    private int mapHeight;
    private int mapInventoryCount;

    private string filePath;

    /// <summary>
    /// Generate or load map from file.
    /// </summary>
    /// <param name="generateMap"></param>
    public MapBuilder(bool generateMap)
    {
        this.generateMap = generateMap ;
    }

    /// <summary>
    /// Set width, height and inventory count for map generation.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="inventoryCount"></param>
    public void SetParametersForGeneration(int width, int height, int inventoryCount)
    {
        if (generateMap)
        {
            this.mapWidth = width;
            this.mapHeight = height;
            this.mapInventoryCount = inventoryCount;
        }
    }

    /// <summary>
    /// Set file path for load map.
    /// </summary>
    /// <param name="path"></param>
    public void SetFilePath(string path)
    {
        if (!generateMap)
            this.filePath = path;
    }
    
    public Map Build()
    {
        return generateMap ? new Map(mapWidth, mapHeight, mapInventoryCount) : MapLoader.LoadFromFile(filePath);
    }
}
