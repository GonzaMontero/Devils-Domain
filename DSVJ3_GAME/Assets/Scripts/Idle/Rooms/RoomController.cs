using System;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public Action RoomDestroy;
    public Action RoomUpdate;
    public Action<RoomController> RoomClicked;
    public BoolAction RoomClickable;
    [SerializeField] RoomData data;
    //int[] roomLimits = new int[4]; //left, right, down, up

    public void Build(/*List<Tile> roomTiles,*/ RoomSO so)
    {
        //Set Data
        data = new RoomData();
        data.so = so;
        data.SetCurrents();
        RoomUpdate?.Invoke();

        //Set Position & Link Tiles
        //foreach (Tile tile in roomTiles)
        //{
        //    //Link Action
        //    RoomIsBeingDestroyed += tile.OnRoomBeingDestroyed;

        //    //Set Room Limits OPTIMIZE
        //    if (tile.X < roomLimits[0])
        //    {
        //        roomLimits[0] = tile.X;
        //    }
        //    if (tile.X > roomLimits[1])
        //    {
        //        roomLimits[1] = tile.X;
        //    }
        //    if (tile.Y < roomLimits[2])
        //    {
        //        roomLimits[2] = tile.Y;
        //    }
        //    if (tile.Y > roomLimits[3])
        //    {
        //        roomLimits[3] = tile.Y;
        //    }
        //}
    }
    public void Destroy()
    {
        RoomDestroy?.Invoke();
        Destroy(gameObject);
    }
    public int GetUpgradeCost()
    {
        if (data.upgradeLvl >= data.so.maxUpgrades) { return -1; }
        return (int)(data.so.baseCost * data.so.updgradeCostMod * Mathf.Pow(data.upgradeLvl, 3) * data.positionCostModifier);
    }
    public int GetBuildCost()
    {
        return data.so.baseCost * data.positionCostModifier;
    }
    public void Upgrade()
    {
        if (data.upgradeLvl >= data.so.maxUpgrades) { return; }

        data.upgradeLvl++;
        data.goldGen = data.so.baseGoldGeneration * (int)Mathf.Pow(data.upgradeLvl, 2);
        RoomUpdate?.Invoke();
    }
    public int GetGoldGen()
    {
        return data.goldGen;
    }
    public Sprite ReturnSprite()
    {
        return data.so.sprite;
    }
    public void StartRoomRaycast(int posCostMod)
    {
        for (int i = 0; i < 8; i++)
        {
            //Raycast
            Vector2 direction = Directions.directions[(int)(Directions.Direction)i];

            //Start new raycast
            RoomController nextRoom = GetNextRoom(direction);
            nextRoom?.UpdatePositionCostModifier(data.positionCostModifier + posCostMod);
            if (Mathf.Abs(direction.x) + Mathf.Abs(direction.y) == 1)
            {
                nextRoom?.ContinuePerpendicularRay((Directions.Direction)i, posCostMod);
            }
            else
            {
                nextRoom?.ContinueDiagonalRay((Directions.Direction)i, posCostMod);
            }
        }
    }
    public void ContinuePerpendicularRay(Directions.Direction dir, int posCostMod)
    {
        RoomController nextRoom = GetNextRoom(Directions.directions[(int)dir]);
        nextRoom?.UpdatePositionCostModifier(data.positionCostModifier + posCostMod);
        nextRoom?.ContinuePerpendicularRay(dir, posCostMod);
    }
    public void ContinueDiagonalRay(Directions.Direction dir, int posCostMod)
    {
        //Raycast to next diagonal Room
        Vector2 direction = Directions.directions[(int)dir]; //get dir vec2
        RoomController nextRoom = GetNextRoom(direction);
        nextRoom?.UpdatePositionCostModifier(data.positionCostModifier + posCostMod);
        nextRoom?.ContinueDiagonalRay(dir, posCostMod);

        //Get previous perpendicular room (up from upRight)
        direction = Directions.directions[(int)dir - 1]; //get dir vec2
        nextRoom = GetNextRoom(direction);
        nextRoom?.UpdatePositionCostModifier(data.positionCostModifier + posCostMod);
        nextRoom?.ContinuePerpendicularRay(dir - 1, posCostMod); //next ray has (up from upRight)

        //Get next perpendicular room (right from upRight)
        direction = Directions.directions[(int)dir + 1]; //get dir vec2
        nextRoom = GetNextRoom(direction);
        nextRoom?.UpdatePositionCostModifier(data.positionCostModifier + posCostMod);
        nextRoom?.ContinuePerpendicularRay(dir + 1, posCostMod); //next ray has (right from upRight)
    }
    public void UpdatePositionCostModifier(int newPosCostMod)
    {
        data.positionCostModifier = newPosCostMod;
    }
    RoomController GetNextRoom(Vector2 dir)
    {
        Vector2 pos = transform.localPosition;
        Vector2 size = transform.GetComponent<BoxCollider2D>().size;
        float distance = (size.x + size.y) * 1.1f;
        RaycastHit2D roomHitted = Physics2D.Raycast(pos, dir, distance, LayerMask.GetMask("Rooms")); //get new room
        if (roomHitted)
        {
            return roomHitted.transform.GetComponent<RoomController>();
        }
        else
        {
            return null;
        }
    }

    private void OnMouseDown()
    {
        if (RoomClickable.Invoke())
        {
            Debug.Log("You clicked an object lmao");
            //hit.transform.GetComponent<UpgradeRoom>().Upgrade()
            RoomClicked?.Invoke(this);
        }
    }
}