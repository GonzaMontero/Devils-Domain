using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Action NotEnoughGold;
    public Action<int> GoldGenerated;
    [SerializeField] RoomSO roomTemplate;
    [SerializeField] float goldGenTime;
    [SerializeField] List<RoomController> rooms; //PUBLIC IS TEMPORAL, CHECK LATER
    [SerializeField] int currentGold; //TEMP VARIABLE, JUST FOR PROTO
    [SerializeField] TMPro.TextMeshProUGUI goldUI; //TEMP VARIABLE, JUST FOR PROTO
    [SerializeField] TMPro.TextMeshProUGUI upgradeUI; //TEMP VARIABLE, JUST FOR PROTO
    [SerializeField] WorldController world;

    private void Start()
    {
        world.RoomGenerated += AddRoomToList;

        //invoke Generate Gold every "goldGenTime" seconds
        InvokeRepeating("GenerateGold", goldGenTime, goldGenTime);

        GoldGenerated += OnGoldGenerated; //TEMP
        upgradeUI.text = "Upgrade\nCost: " + rooms[0].GetUpgradeCost();
    }
    
    #region Methods
    //DateTime is in System
    DateTime logInTime;
    DateTime logOutTime;
    void LoadGameplayScene()
    {
        logInTime = DateTime.Now;
    }
    void ExitGameplayScene()
    {
        logOutTime = DateTime.Now;
    }
    void CalculateAFKGold()
    {
        //TimeSpan is also on System
        TimeSpan afkTime = logOutTime - logInTime;
        for (int i = 0; i < afkTime.TotalSeconds; i++)
        {
            GenerateAFKGold((float)afkTime.TotalSeconds);
        }
    }
    #endregion

    public void UpgradeRoom(int roomSelected = 0)
    {
        RoomController room = rooms[roomSelected];
        int upgradeCost = room.GetUpgradeCost();

        if (upgradeCost > 0 && currentGold >= upgradeCost)
        {
            currentGold -= upgradeCost;
            room.Upgrade();
            GoldGenerated.Invoke(0);
        }
        else
        {
            NotEnoughGold?.Invoke();
        }

        upgradeUI.text = "Upgrade\nCost: " + room.GetUpgradeCost();
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
        GoldGenerated.Invoke((int)(totalGoldGen * secondsPassed));
    }
    void OnGoldGenerated(int goldGenerated) //TEMP
    {
        currentGold += goldGenerated;
        goldUI.text = "Gold: " + currentGold;
    }

    void AddRoomToList(RoomController rc)
    {
        rooms.Add(rc);
    }
}