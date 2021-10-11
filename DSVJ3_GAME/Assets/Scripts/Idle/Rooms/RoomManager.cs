using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Action NotEnoughGold;
    public Action<int> GoldChanged;
    public Action<int> GemsChanged;
    public Action<int> RoomUpdated;
    public Action<RoomController, int> RoomClicked; //TEMP, DELETE INT
    public BoolAction RoomClickable;
    [SerializeField] float gemGenTime;
    [SerializeField] RoomSO[] roomTemplates;
    [SerializeField] List<RoomController> rooms;
    [SerializeField] RoomController roomSelected;
    [SerializeField] RoomCreator world;
    [SerializeField] RoomPlayer player; //TEMP
    bool firstRoomBuilded = false;

    //Unity Methods
    private void Awake()
    {
        //Load Room Templates
        roomTemplates = Resources.LoadAll<RoomSO>("Rooms");

        //Link Actions
        world.RoomGenerated += AddRoomToList;

        //invoke Generate Gems every "goldGenTime" seconds, multiplied by 60 to get minutes
        InvokeRepeating("GenerateGems", gemGenTime * 60, gemGenTime * 60);
    }
    private void OnDestroy()
    {
        foreach (RoomSO template in roomTemplates)
        {
            Resources.UnloadAsset(template);
        }
    }

    //Methods
    public List<RoomData> GetRooms()
    {
        List<RoomData> roomDatas = new List<RoomData>();
        foreach (RoomController room in rooms)
        {
            roomDatas.Add(room.GetData());
        }
        return roomDatas;
    }
    public void LoadRooms(List<RoomData> roomDatas)
    {
        for (int i = 0; i < roomDatas.Count; i++)
        {
            rooms[i].RoomClicked += OnRoomClicked;
            rooms[i].RoomClickable += OnRoomClickable;
            rooms[i].LoadData(roomDatas[i]);
        }
    }
    public void UpgradeRoom()
    {
        int upgradeCost = roomSelected.GetUpgradeCost();

        if (upgradeCost > 0 && player.playerData.gold >= upgradeCost) //TEMP, REPLACE PLAYERGOLD FOR (ACTION?)
        {
            GoldChanged?.Invoke(-roomSelected.GetUpgradeCost());
            roomSelected.Upgrade();
            RoomUpdated?.Invoke(roomSelected.GetUpgradeCost());
        }
        else
        {
            NotEnoughGold?.Invoke();
        }
    }
    public void BuildRoom()
    {
        int buildCost = roomTemplates[1].baseCost; //TEMP, MAKE SYSTEM TO SELECT DIFF ROOMS

        if (buildCost > 0 && player.playerData.gold >= buildCost) //TEMP, REPLACE PLAYERGOLD FOR (ACTION?)
        {
            GoldChanged.Invoke(-buildCost);
            roomSelected.Build(roomTemplates[1]);
            RoomUpdated.Invoke(buildCost);

            if (!firstRoomBuilded)
            {
                firstRoomBuilded = true;
                SetRoomsPrice();
            }
        }
        else
        {
            NotEnoughGold?.Invoke();
        }
    }
    void GenerateGems()
    {
        GenerateAFKGems(gemGenTime);
    }
    void GenerateAFKGems(float secondsPassed)
    {
        int totalGemGen = 0;
        foreach (RoomController room in rooms)
        {
            totalGemGen += room.GetGemGen();
        }
        GemsChanged?.Invoke((int)(totalGemGen * secondsPassed));
    }
    void AddRoomToList(RoomController rc)
    {
        rc.Build(roomTemplates[0]);
        rooms.Add(rc);
        rc.RoomClicked += OnRoomClicked;
        rc.RoomClickable += OnRoomClickable;
    }
    void SetRoomsPrice()
    {
        roomSelected.StartRoomRaycast(1);
    }

    //Action Receivers
    void OnRoomClicked(RoomController rc)
    {
        roomSelected = rc;
        RoomClicked?.Invoke(rc, roomTemplates[1].baseCost); //TEMP, DELETE BUILD COST
    }
    bool OnRoomClickable()
    {
        return RoomClickable.Invoke();
    }
}