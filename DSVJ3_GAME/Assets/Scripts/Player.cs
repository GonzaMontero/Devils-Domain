using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviourSingleton<Player>
{
    //Actions
    public Action GoldChanged;
    public Action GemsChanged;
    public Action LevelChanged;
   
    public PlayerData templateData { private get; set; } //backupData, what is in the inspector
    
    //Rooms
    public RoomData room
    {
        get { return data.room; }
        set { data.room = value; }
    }
    //Characters
    public List<BattleCharacterData> characters
    {
        get { return data.characters; }
        set { data.characters = value; }
    }
    public BattleCharacterData[] lineup
    {
        get { return data.lineup; }
        set { data.lineup = value; }
    }    
    public int level
    {
        get { return data.level; }
        set { if (value < 0) value = 0; data.level = value; LevelChanged?.Invoke(); }
    }
    //Menu
    public int tutorialStep
    {
        get { return data.tutorialStep; }
        set { if (value < 1) value = 1; data.tutorialStep = value;  }
    }
    //Settings
    public SettingsData settings
    {
        get { return data.settings; }
        set { data.settings = value; }
    }
    //General
    public int gold
    {
        get { return data.gold; }
        set 
        { 
            if (value < 0) value = 0; 
            else if (value > 99999) value = 99999;
            data.gold = value; 
            GoldChanged?.Invoke();
        }
    }
    public int gems
    {
        get { return data.gems; }
        set
        {
            if (value < 0) value = 0;
            else if (value > 99999) value = 99999;
            data.gems = value;
            GemsChanged?.Invoke();
        }
    }
    public string playerName
    {
        get { return data.name; }
        private set { data.name = value; }
    }
    [SerializeField] PlayerData data;


    //Unity Events
    private void Start()
    {
        templateData = data;
        RecieveData();
        AudioManager.ChangeGeneralVolume(settings.generalVolume);
        AudioManager.ChangeMusicVolume(settings.musicVolume);
        AudioManager.ChangeFXVolume(settings.fxVolume);
    }
    //private void OnDestroy()
    //{
    //    if (Player.Get() != this)
    //    {
    //        Player.Get().templateData = this.templateData;
    //    }
    //}

    //Methods
    public void SaveData()
    {
        string dataJSON = JsonUtility.ToJson(data);
        FileManager<string>.SaveDataToFile(dataJSON, Application.persistentDataPath + " data.bin");
    }
    public void RecieveData()
    {
        PlayerData temp = new PlayerData();
        string dataJSON;
        dataJSON = FileManager<string>.LoadDataFromFile(Application.persistentDataPath + " data.bin");
        JsonUtility.FromJsonOverwrite(dataJSON, temp);
        if (temp.lineup[0] != null && temp.lineup[0].so != null)
        {
            data = temp;
        }
        else
        {
            data = templateData;
        }
    }
    public void DeleteData()
    {
        FileManager<string>.DeleteFile(Application.persistentDataPath + " data.bin");
        data = templateData;
    }
    public void SaveLogOutDate()
    {
        data.roomLogOutTime = DateTime.Now.ToBinary().ToString();
    }
    public float GetAFKMinutes()
    {
        if (data.roomLogOutTime == "") return 0;

        //Get from file
        long temp = Convert.ToInt64(data.roomLogOutTime);
        DateTime logOutTime = DateTime.FromBinary(temp);

        //Calculate span
        TimeSpan afkTime = DateTime.Now - logOutTime;

        //Save time since last checked
        SaveLogOutDate();

        //Return
        return (float)(afkTime.TotalSeconds / 60); //calculate in minutes
    }

    //Event Receivers
    public void OnNameEdit(string name)
    {
        playerName = name;
    }

    public void SwapPositions(int positionInArray, BattleCharacterData characterToSwap)
    {
        BattleCharacterData placeHolder;
        placeHolder = lineup[positionInArray];      
        lineup[positionInArray] = characterToSwap;

        if (placeHolder.so != null)
        {
            var index = characters.IndexOf(characterToSwap);
            characters[index] = placeHolder;
        }
        else
        {
            characters.Remove(characterToSwap);
        }
    }
}