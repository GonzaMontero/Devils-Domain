using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public List<BattleCharacterData> characters;
    public BattleCharacterData[] lineup = new BattleCharacterData[6];
    public int gold;
    public int gems;
    public string name;

}