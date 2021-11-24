using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Action NotEnoughGold;
    public Action<int, bool> RoomLoad;
    public Action<int> RoomUpdate;
    [SerializeField] float gemGenTime;
    [SerializeField] RoomSO roomTemplate;
    [SerializeField] RoomController room;
    Player player;

    //Unity Methods
    private void Awake()
    {
        //invoke Generate Gems every "goldGenTime" seconds, multiplied by 60 to get minutes
        InvokeRepeating("GenerateGems", gemGenTime * 60, gemGenTime * 60);
    }
    private void Start()
    {
        player = Player.Get(); //Get Player
        InitRoom(); //Load RoomData into room
        GenerateAFKGems(player.GetAFKMinutes()); //Start GenGems frecuent invocation
    }
    private void OnDestroy()
    {
        player.room = room.GetData();
    }

    //Methods
    public void UpgradeRoom()
    {
        int upgradeCost = room.GetUpgradeCost();
        if (upgradeCost <= 0) return;
        
        if (player.gold >= upgradeCost)
        {
            player.gold -= room.GetUpgradeCost();
            room.Upgrade();
            player.room = room.GetData();
            RoomUpdate?.Invoke(room.GetUpgradeCost());
        }
        else
        {
            NotEnoughGold?.Invoke();
        }
    }
    public void BuildRoom()
    {
        int buildCost = roomTemplate.baseCost;

        if (buildCost > 0 && player.gold >= buildCost)
        {
            player.gold -= buildCost;
            room.Build(roomTemplate);
            player.room = room.GetData();
            RoomUpdate.Invoke(room.GetUpgradeCost());
        }
        else
        {
            NotEnoughGold?.Invoke();
        }
    }
    public void GenerateAFKGems(float minutesPassed)
    {
        player.gems += (int)(room.GetGemGen() * minutesPassed);
    }
    void GenerateGems()
    {
        GenerateAFKGems(gemGenTime);
    }
    void InitRoom()
    {
        if (player.room.so != null)
        {
            room.LoadData(player.room);
            RoomLoad?.Invoke(room.GetUpgradeCost(), true);
        }
        else
        {
            RoomLoad?.Invoke(roomTemplate.baseCost, false);
        }
    }
}