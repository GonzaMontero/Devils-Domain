using System.Collections.Generic;

public class Player : MonoBehaviourSingleton<Player>
{
    public List<BattleCharacterData> characters;
    public BattleCharacterData[] lineup = new BattleCharacterData[6];
    public int gold;
    public int gems;
    string playerName;

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