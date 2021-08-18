using UnityEngine;
using System;
public class PlayerManager : MonoBehaviourSingleton<PlayerManager>
{
    public struct Data
    {
        public int gold;
        public int level;
        public DateTime logInTime;
        public DateTime logOutTime;
    }
    public Data playerData;
    public void Start()
    {
        RecieveData();
    }
    public void OnApplicationQuit()
    {
        SaveData();
    }
    public void SaveData()
    {
        playerData.logOutTime = DateTime.Now;
        PlayerPrefs.SetInt("Gold", playerData.gold);
        PlayerPrefs.SetInt("Level", playerData.level);
        PlayerPrefs.Save();
    }
    public void RecieveData()
    {
        playerData.gold = PlayerPrefs.GetInt("Gold");
        playerData.level = PlayerPrefs.GetInt("Level");
        playerData.logInTime = DateTime.Now;
    }
}
