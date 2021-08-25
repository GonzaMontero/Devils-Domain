using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Action NotEnoughGold;
    public Action<int> GoldChanged;
    public Action<int> RoomUpdated;
    public Action<RoomController, int> RoomClicked; //TEMP, DELETE INT
    public BoolAction RoomClickable;
    [SerializeField] float goldGenTime;
    [SerializeField] RoomSO[] roomTemplates;
    [SerializeField] List<RoomController> rooms;
    [SerializeField] RoomController roomSelected;
    [SerializeField] WorldController world;
    [SerializeField] PlayerManager player; //TEMP
    bool firstRoomBuilded = false;

    //Unity Methods
    private void Awake()
    {
        //Load Room Templates
        roomTemplates = Resources.LoadAll<RoomSO>("Rooms");

        //Link Actions
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

    //Methods
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
        GoldChanged?.Invoke((int)(totalGoldGen * secondsPassed));
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