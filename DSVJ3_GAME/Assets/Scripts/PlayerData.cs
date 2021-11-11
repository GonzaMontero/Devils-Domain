using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    //General Data
    public int gold;
    public int gems;
    public string name;

    //Autobattle Data
    public List<BattleCharacterData> characters;
    public BattleCharacterData[] lineup = new BattleCharacterData[6];

    //Room Data
    public DateTime roomLogInTime;
    public DateTime roomLogOutTime;
    public List<RoomData> rooms;
}