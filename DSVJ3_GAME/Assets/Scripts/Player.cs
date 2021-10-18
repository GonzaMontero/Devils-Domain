using System.Collections.Generic;

public class Player : MonoBehaviourSingleton<Player>
{
    public List<BattleCharacterData> characters;
    public BattleCharacterData[] lineup = new BattleCharacterData[6];
    public int gold;
    public int gems;
    string playerName;

    private void Start()
    {
        characters = new List<BattleCharacterData>();
    }

    public void OnNameEdit(string name)
    {
        playerName = name;
    }

    public void SwapPositions(int positionInArray, BattleCharacterData characterToSwap)
    {
        lineup[positionInArray] = characterToSwap;
    }
}