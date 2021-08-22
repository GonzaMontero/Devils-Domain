using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
	public Action RoomDestroy;
	public Action RoomUpdate;
	[SerializeField] RoomData data;
	int[] roomLimits = new int[4];

    private void Awake()
    {
        data = new RoomData();
        data.so = Resources.Load<RoomSO>("Rooms/EmptyRoom");
        data.SetCurrents();
    }

    public void Build(List<Tile> roomTiles, RoomSO so)
	{
        data.so = so;

        foreach (Tile tile in roomTiles)
        {
            //Link Action
            //RoomIsBeingDestroyed += tile.OnRoomBeingDestroyed;
            
            //Set Room Limits       OPTIMIZE
            //if (tile.X < roomLimits[0])
            //{
            //    roomLimits[0] = tile.X;
            //}
            //if (tile.X > roomLimits[1])
            //{
            //    roomLimits[1] = tile.X;
            //}
            //if (tile.Y < roomLimits[2])
            //{
            //    roomLimits[2] = tile.Y;
            //}
            //if (tile.Y > roomLimits[3])
            //{
            //    roomLimits[3] = tile.Y;
            //}
        }
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
	public void Upgrade()
    {
        if (data.upgradeLvl >= data.so.maxUpgrades) { return; }

        RoomUpdate?.Invoke();
        data.upgradeLvl++;
        data.goldGen = data.so.baseGoldGeneration * data.upgradeLvl * data.upgradeLvl;
    }
	public int GetGoldGen()
    {
        return data.goldGen;
    }
    public Sprite ReturnSprite()
    {
        return data.so.sprite;
    }
}