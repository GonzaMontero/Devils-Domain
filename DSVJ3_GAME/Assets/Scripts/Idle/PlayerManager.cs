using UnityEngine;
using System;
public class PlayerManager : MonoBehaviourSingleton<PlayerManager>
{
    public Action<int> GoldChanged;
    [SerializeField] RoomManager roomManager;

    [Serializable] public struct Data
    {
        public int gold;
        public int level;
        public DateTime logInTime;
        public DateTime logOutTime;
    }
    public Data playerData;
    public void Start()
    {
        roomManager.GoldChanged += OnGoldChanged;
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
        playerData.gold = 50;//PlayerPrefs.GetInt("Gold");
        playerData.level = PlayerPrefs.GetInt("Level");
        playerData.logInTime = DateTime.Now;
    }
    private void OnGoldChanged(int gold)
    {
        playerData.gold += gold;
        GoldChanged?.Invoke(playerData.gold);
    }


    //CHECK LATER
    #region MethodsForAFK
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
    public void CalculateAFKGold() //this may go on roomManager
    {
        //TimeSpan is also on System
        TimeSpan afkTime = logOutTime - logInTime;
        for (int i = 0; i < afkTime.TotalSeconds; i++)
        {
            //GenerateAFKGold((float)afkTime.TotalSeconds);
        }
    }
    #endregion 
}
