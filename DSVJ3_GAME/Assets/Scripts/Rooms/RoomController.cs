using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
	public Action RoomDestroy;
	public Action RoomUpdate;
	[SerializeField] RoomData data;
	int[] roomLimits = new int[4];

    private void Start()
    {
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
            if (tile.X < roomLimits[0])
            {
                roomLimits[0] = tile.X;
            }
            if (tile.X > roomLimits[1])
            {
                roomLimits[1] = tile.X;
            }
            if (tile.Y < roomLimits[2])
            {
                roomLimits[2] = tile.Y;
            }
            if (tile.Y > roomLimits[3])
            {
                roomLimits[3] = tile.Y;
            }
        }
	}
	public void Destroy()
    {
		RoomDestroy?.Invoke();
        Destroy(gameObject);
    }
    public int GetUpgradeCost()
    {
        return (int)Mathf.Pow(data.so.baseCost * data.so.updgradeCostMod, data.upgradeLvl);
    }
	public void Upgrade()
    {
        RoomUpdate?.Invoke();
        data.upgradeLvl++;
        data.goldGen *= data.so.baseGoldGeneration;
    }
	public int GetGoldGen()
    {
        return data.goldGen;
    }
}