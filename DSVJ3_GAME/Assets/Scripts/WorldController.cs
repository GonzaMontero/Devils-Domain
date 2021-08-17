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

        if(offset <= 0)
        {
            offset = 5;
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
                tile_go.transform.position = new Vector3(tile_data.X + offset, tile_data.Y, 0);
                
                SpriteRenderer tile_sprRend= tile_go.AddComponent<SpriteRenderer>();           

                tile_sprRend.sprite = occupiedSprite;

                tile_go.layer = LayerMask.NameToLayer("Rooms");

                tile_go.AddComponent<BoxCollider2D>();

                tile_go.transform.localScale = new Vector3(offset, 1, 0);
            }
        }
    }

    void Update()
    {
        
    }
}
