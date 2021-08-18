using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Action NotEnoughGold;
    public Action<int> GoldGenerated;
    [SerializeField] RoomSO roomTemplate;
    [SerializeField] float goldGenTime;
    public List<RoomController> rooms; //PUBLIC IS TEMPORAL, CHECK LATER
    [SerializeField] int currentGold; //TEMP VARIABLE, JUST FOR PROTO
    [SerializeField] TMPro.TextMeshProUGUI goldUI; //TEMP VARIABLE, JUST FOR PROTO

    private void Start()
    {
        //invoke Generate Gold every "goldGenTime" seconds
        InvokeRepeating("GenerateGold", goldGenTime, goldGenTime);

        GoldGenerated += OnGoldGenerated; //TEMP
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

        if (/*gold*/ currentGold > upgradeCost)
        {
            currentGold -= upgradeCost;
            room.Upgrade();
            GoldGenerated.Invoke(0);
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
        GoldGenerated.Invoke((int)(totalGoldGen * secondsPassed));
    }
    void OnGoldGenerated(int goldGenerated) //TEMP
    {
        currentGold += goldGenerated;
        goldUI.text = "Gold: " + currentGold.ToString("000");
    }
}