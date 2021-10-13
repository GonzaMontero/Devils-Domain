using UnityEngine;
using System;
using System.Collections.Generic;

public class RoomPlayer : MonoBehaviour
{
    public Action<int> GoldChanged;
    public Action<int> GemsChanged;
    [SerializeField] RoomManager roomManager;
    Player player;

    [Serializable] public struct Data
    {
        public int gold;
        public int gems;
        public DateTime logInTime;
        public DateTime logOutTime;
        public List<RoomData> rooms;
    }
    public Data playerData;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
        roomManager.GoldChanged += OnGoldChanged;
        roomManager.GemsChanged += OnGemsChanged;
        RecieveData();
    }
    private void OnDestroy()
    {
        SaveData();
    }
    public void SaveData()
    {
        playerData.logOutTime = DateTime.Now;
        player.gems += playerData.gems;
        player.gold = playerData.gold;
        playerData.rooms = roomManager.GetRooms();
        string dataJSON = JsonUtility.ToJson(playerData);
        FileManager<string>.SaveDataToFile(dataJSON, Application.persistentDataPath + " data.bin");
    }
    public void RecieveData()
    {
        string dataJSON;
        dataJSON = FileManager<string>.LoadDataFromFile(Application.persistentDataPath + " data.bin");
        JsonUtility.FromJsonOverwrite(dataJSON, playerData);
        OnGoldChanged(0);
        OnGoldChanged(0);
        playerData.logInTime = DateTime.Now;
        roomManager.LoadRooms(playerData.rooms);
        CalculateAFKGems();
    }

    private void OnGoldChanged(int gold)
    {
        playerData.gold += gold;
        GoldChanged?.Invoke(playerData.gold);
    }
    private void OnGemsChanged(int gems)
    {
        playerData.gems += gems;
        GemsChanged?.Invoke(playerData.gems);
    }


    //CHECK LATER
    #region MethodsForAFK
    public void CalculateAFKGems() //this may go on roomManager
    {
        //TimeSpan is also on System
        TimeSpan afkTime = playerData.logOutTime - playerData.logInTime;
        roomManager.GenerateAFKGems((float)afkTime.TotalSeconds / 60); //calculate in minutes
    }
    #endregion 
}
