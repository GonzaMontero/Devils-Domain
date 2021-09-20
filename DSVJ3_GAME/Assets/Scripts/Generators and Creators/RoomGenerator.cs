using UnityEngine;

public class RoomGenerator
{
    Tile[,] tiles;

    [Header("Room Size")]
    int width;
    int height;

    #region Getters
    public int Width
    {
        get
        {
            return width;
        }
    }
    public int Height
    {
        get
        {
            return height;
        }
    }
    #endregion

    public RoomGenerator(int width = 10, int height = 10)
    {
        this.width = width;
        this.height = height;

        tiles = new Tile[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = new Tile( x, y);
            }
        }

        Debug.Log("World Generated With width=" + width + " || height=" + height);
    }
    public Tile GetTileAt(int x, int y)
    {
        return tiles[x, y];
    }
}
