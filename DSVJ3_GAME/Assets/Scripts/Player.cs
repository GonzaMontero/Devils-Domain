using System.Collections.Generic;

public class Player : MonoBehaviourSingleton<Player>
{
    public List<BattleCharacterData> characters;
    public BattleCharacterData[] lineup = new BattleCharacterData[6];
    private void Start()
    {
        characters = new List<BattleCharacterData>();
    }
}
