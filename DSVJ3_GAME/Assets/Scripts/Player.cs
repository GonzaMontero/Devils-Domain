using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviourSingleton<Player>
{
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
    public int gold
    {
        get { return data.gold; }
        set { data.gold = value; if (data.gold < 0) data.gold = 0; }
    }
    public int gems
    {
        get { return data.gems; }
        set { data.gems = value; if (data.gems < 0) data.gems = 0; }
    }
    [SerializeField] PlayerData data;
    string playerName
    {
        get { return data.name; }
        set { data.name = value; }
    }

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