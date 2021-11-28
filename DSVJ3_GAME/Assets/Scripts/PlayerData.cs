using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    //General Data
    public int gold;
    public int gems;
    public string name;

    //Menu Data
    public int tutorialStep = 1;

    //Settings Data
    public SettingsData settings;

    //Autobattle Data
    public List<BattleCharacterData> characters;
    public BattleCharacterData[] lineup = new BattleCharacterData[6];
    public int level;

    //Room Data
    public string roomLogOutTime;
    public RoomData room;
}