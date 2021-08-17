using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int height;

    [SerializeField] float offset;

    [SerializeField] Sprite occupiedSprite;

    World world;

    
    void Start()
    {
        if (width <= 0 || height <= 0)
        {
            world = new World();
        }
        else
        {
            world = new World(width, height);
        }

        world.RandomizeTiles();

        for (int x = 0; x < world.Width; x++)
        {
            for (int y = 0; y < world.Height; y++)
            {
                Tile tile_data = world.GetTileAt(x, y);
                GameObject tile_go = new GameObject();
                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.parent = this.transform;
                tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y, 0);
                
                SpriteRenderer tile_sprRend= tile_go.AddComponent<SpriteRenderer>();           

                if (tile_data.Type == Tile.TileType.Occupied)
                    tile_sprRend.sprite = occupiedSprite;
            }
        }
    }

    void Update()
    {
        
    }
}
