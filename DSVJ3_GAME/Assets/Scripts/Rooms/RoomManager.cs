using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Action NotEnoughGold;
    public Action<int> GoldGenerated;
    public Action<int> RoomUpdated;
    public Action<RoomController> RoomClicked;
    [SerializeField] float goldGenTime;
    [SerializeField] RoomSO[] roomTemplates;
    [SerializeField] List<RoomController> rooms;
    [SerializeField] WorldController world;
    [SerializeField] PlayerManager player; //TEMP

    private void Awake()
    {
        //Load Room Templates
        roomTemplates = Resources.LoadAll<RoomSO>("Rooms");

        world.RoomGenerated += AddRoomToList;

        //invoke Generate Gold every "goldGenTime" seconds
        InvokeRepeating("GenerateGold", goldGenTime, goldGenTime);
    }
    private void OnDestroy()
    {
        foreach (RoomSO template in roomTemplates)
        {
            Resources.UnloadAsset(template);
        }
    }

    public void UpgradeRoom(int roomSelected = 0)
    {
        RoomController room = rooms[roomSelected];
        int upgradeCost = room.GetUpgradeCost();

        if (upgradeCost > 0 && player.playerData.gold >= upgradeCost) //TEMP, REPLACE PLAYERGOLD FOR (ACTION?)
        {
            room.Upgrade();
            RoomUpdated.Invoke(upgradeCost);
        }
        else
        {
            NotEnoughGold?.Invoke();
        }
    }
    void GenerateGold()
    {
        GenerateAFKGold(goldGenTime);
    }
    void GenerateAFKGold(float secondsPassed)
    {
        int totalGoldGen = 0;
        foreach (RoomController room in rooms)
        {
            totalGoldGen += room.GetGoldGen();
        }
        GoldGenerated?.Invoke((int)(totalGoldGen * secondsPassed));
    }
    void AddRoomToList(RoomController rc)
    {
        rc.Build(roomTemplates[0]);
        rooms.Add(rc);
        rc.RoomClicked += OnRoomClicked;
    }
    void OnRoomClicked(RoomController rc)
    {
        RoomClicked?.Invoke(rc);
    }
}