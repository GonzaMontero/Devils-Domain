public class Tile
{
    public enum TileType { Empty, Occupied }

    TileType type = TileType.Empty;

    public TileType Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }

    World world;
    int x;
    int y;

    public int X
    {
        get
        {
            return x;
        }
    }
    public int Y
    {
        get
        {
            return y;
        }
    }

    public Tile(World world, int x, int y)
    {
        this.world = world;
        this.x = x;
        this.y = y;
    }
}
