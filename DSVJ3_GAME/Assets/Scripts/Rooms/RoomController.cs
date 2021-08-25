using System;
using System.Collections.Generic;
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
        return (int)(data.so.baseCost * data.so.updgradeCostMod * data.upgradeLvl * data.upgradeLvl);
    }
    public int GetBuildCost()
    {
        return data.so.baseCost;
    }
    public void Upgrade()
    {
        if (data.upgradeLvl >= data.so.maxUpgrades) { return; }

        data.upgradeLvl++;
        data.goldGen = data.so.baseGoldGeneration * data.upgradeLvl * data.upgradeLvl;
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