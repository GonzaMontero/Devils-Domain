using UnityEngine;

public class SlotsGeneration
{
    Tile[,] playerTiles;
    Tile[,] enemyTiles;

    [Header("Battle Grid Size")]
    [SerializeField] int playerWidth;
    [SerializeField] int playerHeight;
    [SerializeField] int enemyWidth;
    [SerializeField] int enemyHeight;

    #region Getters
    public int PlayerWidth
    {
        get
        {
            return playerWidth;
        }
    }
    public int PlayerHeight
    {
        get
        {
            return playerHeight;
        }
    }
    public int EnemyWidth
    {
        get
        {
            return enemyWidth;
        }
    }
    public int EnemyHeight
    {
        get
        {
            return enemyHeight;
        }
    }
    #endregion 
    public SlotsGeneration(int playerWidth=3, int playerHeight=3,int enemyWidth=3,int enemyHeight = 3)
    {
        this.playerWidth = playerWidth;
        this.playerHeight = playerHeight;
        playerTiles = new Tile[playerWidth, playerHeight];
        for (int x = 0; x < playerWidth; x++)
        {
            for (int y = 0; y < playerHeight; y++)
            {
                playerTiles[x, y] = new Tile(x, y);
            }
        }

        this.enemyWidth = enemyWidth;
        this.enemyHeight = enemyHeight;
        enemyTiles = new Tile[enemyWidth, EnemyHeight];
        for (int x = 0; x < enemyWidth; x++)
        {
            for (int y = 0; y < EnemyHeight; y++)
            {
                playerTiles[x, y] = new Tile(x, y);
            }
        }
    }
    public Tile GetPlayerTileAt(int x, int y)
    {
        return playerTiles[x, y];
    }
    public Tile GetEnemyTileAt(int x, int y)
    {
        return enemyTiles[x, y];
    }
}
