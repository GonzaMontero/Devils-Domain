using UnityEngine;
using System;

public class WorldController : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int height;

    [SerializeField] float sizeX;
    [SerializeField] float sizeY;

    [SerializeField] GameObject roomUIPrefab;
    [SerializeField] Transform roomUIHolder;

    World world;
    public Action<RoomController> RoomGenerated;

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

        if (sizeX <= 0)
        {
            sizeX = 5;
        }
        if (sizeY <= 0)
        {
            sizeY = 1;
        }

        world.RandomizeTiles();

        for (int x = 0; x < world.Width; x++)
        {
            for (int y = 0; y < world.Height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y)
    {
        //Get Tile
        Tile tile_data = world.GetTileAt(x, y);

        //Generate GO
        GameObject tile_go = new GameObject();
        tile_go.name = "Tile_" + x + "_" + y;
        tile_go.transform.parent = this.transform;
        tile_go.layer = LayerMask.NameToLayer("Rooms");

        //Add RoomController
        RoomController rc = tile_go.AddComponent<RoomController>();
        RoomGenerated?.Invoke(rc);

        //Add Sprite
        SpriteRenderer tile_sprRend = tile_go.AddComponent<SpriteRenderer>();
        tile_sprRend.sprite = rc.ReturnSprite();
        tile_sprRend.size = new Vector2(sizeX, sizeY);

        //Add Collider
        BoxCollider2D tile_boxColl = tile_go.AddComponent<BoxCollider2D>();
        //tile_boxColl.size = new Vector2(sizeX, sizeY);

        //Reset Position
        tile_go.transform.position = new Vector3(tile_data.X * tile_boxColl.size.x, tile_data.Y * tile_boxColl.size.y, 0);
    
        //Add Room UI
        Transform roomUI = Instantiate(roomUIPrefab, tile_go.transform).transform;
        roomUI.parent = roomUIHolder; //change parent so pos=tile & parent=canvas
        roomUI.GetComponent<RoomUI>().controller = rc;
    }
}