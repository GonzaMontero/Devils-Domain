using UnityEngine;
using System;

public class SlotsCreator : MonoBehaviour
{
    public Action<BoxCollider2D> SlotGenerated;

    [SerializeField] int width;
    [SerializeField] int height;

    [SerializeField] float sizeX;
    [SerializeField] float sizeY;

    [SerializeField] int posOffsetX;
    [SerializeField] int posOffsetY;

    [SerializeField] Sprite baseSprite;

    SlotsGeneration slots;

    void Start()
    {
        if (width <= 0 || height <= 0)
        {
            slots = new SlotsGeneration();
        }
        else
        {
            slots = new SlotsGeneration(width, height);
        }
        if (sizeX <= 0)
        {
            sizeX = 5;
        }
        if (sizeY <= 0)
        {
            sizeY = 1;
        }
        for (int x = 0; x < slots.PlayerWidth; x++)
        {
            for (int y = 0; y < slots.PlayerHeight; y++)
            {
                GenerateAllySlot(x, y);
            }
        }
        for (int x = 0; x < slots.EnemyWidth; x++)
        {
            for (int y = 0; y < slots.EnemyHeight; y++)
            {
                GenerateEnemySlot(x, y);
            }
        }
    }
    void GenerateAllySlot(int x, int y)
    {
        //Get Tile
        Tile tile_data = slots.GetPlayerTileAt(x, y);

        //Generate GO
        GameObject tile_go = new GameObject();
        tile_go.name = "Tile_" + x + "_" + y;
        tile_go.transform.parent = this.transform;
        tile_go.layer = LayerMask.NameToLayer("Slots");
        tile_go.tag = "Slot";

        //Add Sprite Renderer
        SpriteRenderer tile_sprRend = tile_go.AddComponent<SpriteRenderer>();
        tile_sprRend.sprite = baseSprite;
        tile_sprRend.size = new Vector2(sizeX, sizeY);

        //Add Collider
        BoxCollider2D tile_boxColl = tile_go.AddComponent<BoxCollider2D>();

        //Reset Position
        tile_go.transform.position = new Vector3(Camera.main.transform.position.x + sizeX + tile_data.X * tile_boxColl.size.x + posOffsetX, Camera.main.transform.position.y - sizeY + tile_data.Y * tile_boxColl.size.y + posOffsetY, 0);

        //Invoke action Slot Generated and send the boxCollider as ref
        SlotGenerated?.Invoke(tile_boxColl);
    }
    void GenerateEnemySlot(int x, int y)
    {
        //Get Tile
        Tile tile_data = slots.GetPlayerTileAt(x, y);

        //Generate GO
        GameObject tile_go = new GameObject();
        tile_go.name = "Tile_" + x + "_" + y;
        tile_go.transform.parent = this.transform;
        tile_go.layer = LayerMask.NameToLayer("Slots");

        //Add Sprite Renderer
        SpriteRenderer tile_sprRend = tile_go.AddComponent<SpriteRenderer>();
        tile_sprRend.sprite = baseSprite;
        tile_sprRend.size = new Vector2(sizeX, sizeY);

        //Add Collider
        BoxCollider2D tile_boxColl = tile_go.AddComponent<BoxCollider2D>();

        //Reset Position
        tile_go.transform.position = new Vector3(Camera.main.transform.position.x - sizeX - tile_data.X * tile_boxColl.size.x - posOffsetX, Camera.main.transform.position.y - sizeY + tile_data.Y * tile_boxColl.size.y + posOffsetY, 0);

        //Invoke action Slot Generated and send the boxCollider as ref
        SlotGenerated?.Invoke(tile_boxColl); 
    }
}
