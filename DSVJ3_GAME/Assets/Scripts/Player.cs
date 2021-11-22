using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviourSingleton<Player>
{
    //Actions
    public Action GoldChanged;
    public Action GemsChanged;
    //Rooms
    public List<RoomData> rooms
    {
        get { return data.rooms; }
        set { data.rooms = value; }
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
        set { if (value < 0) value = 0; data.level = value; }
    }
    //Menu
    public int tutorialStep
    {
        get { return data.tutorialStep; }
        set { if (value < 1) value = 1; data.tutorialStep = value;  }
    }
    //General
    public int gold
    {
        get { return data.gold; }
        set { data.gold = value; if (data.gold < 0) data.gold = 0; GoldChanged?.Invoke(); }
    }
    public int gems
    {
        get { return data.gems; }
        set { data.gems = value; if (data.gems < 0) data.gems = 0; GemsChanged.Invoke(); }
    }
    [SerializeField] PlayerData data;
    string playerName
    {
        get { return data.name; }
        set { data.name = value; }
    }

    //Unity Events
    private void Start()
    {
        //RecieveData();
    }
    private void OnDestroy()
    {
        //SaveData();
    }

    //Methods
    public void SaveData()
    {
        string dataJSON = JsonUtility.ToJson(data);
        FileManager<string>.SaveDataToFile(dataJSON, Application.persistentDataPath + " data.bin");
    }
    public void RecieveData()
    {
        string dataJSON;
        dataJSON = FileManager<string>.LoadDataFromFile(Application.persistentDataPath + " data.bin");
        JsonUtility.FromJsonOverwrite(dataJSON, data);
    }
    public void SaveLogInDate()
    {
        data.roomLogInTime = DateTime.Now;
    }
    public void SaveLogOutDate()
    {
        data.roomLogOutTime = DateTime.Now;
    }
    public float GetAFKMinutes()
    {
        TimeSpan afkTime = data.roomLogOutTime - data.roomLogInTime;
        return((float)afkTime.TotalSeconds / 60); //calculate in minutes
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