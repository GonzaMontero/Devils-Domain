using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Action NotEnoughGold;
    public Action<int> RoomUpdated;
    public Action<RoomController> RoomClicked;
    //public BoolAction RoomClickable;
    [SerializeField] float gemGenTime;
    [SerializeField] RoomSO[] roomTemplates;
    [SerializeField] List<RoomController> rooms;
    [SerializeField] RoomController roomSelected;
    [SerializeField] RoomCreator world;
    Player player;
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
    private void Start()
    {
        player = Player.Get();
        LoadRooms(player.rooms);
        GenerateAFKGems(player.GetAFKMinutes());
    }
    private void OnDestroy()
    {
        player.rooms = GetRooms();
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
            rooms[i].LoadData(roomDatas[i]);
        }
    }
    public void UpgradeRoom()
    {
        int upgradeCost = roomSelected.GetUpgradeCost();
        if (upgradeCost <= 0) return;
        
        if (player.gold >= upgradeCost)
        {
            player.gold -= roomSelected.GetUpgradeCost();
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
        int buildCost = roomTemplates[1].baseCost;

        if (buildCost > 0 && player.gold >= buildCost)
        {
            player.gold -= buildCost;
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
    public void GenerateAFKGems(float minutesPassed)
    {
        int totalGemGen = 0;
        foreach (RoomController room in rooms)
        {
            totalGemGen += room.GetGemGen();
        }
        player.gems += (int)(totalGemGen * minutesPassed);
    }
    void GenerateGems()
    {
        GenerateAFKGems(gemGenTime);
    }
    void AddRoomToList(RoomController rc)
    {
        rc.Build(roomTemplates[0]);
        rooms.Add(rc);
        rc.RoomClicked += OnRoomClicked;
    }
    void SetRoomsPrice()
    {
        roomSelected.StartRoomRaycast(1);
    }

    //Action Receivers
    void OnRoomClicked(RoomController rc)
    {
        roomSelected = rc;
        RoomClicked?.Invoke(rc);
    }
}