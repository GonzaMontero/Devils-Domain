using System.Collections.Generic;

public class Player : MonoBehaviourSingleton<Player>
{
    public List<BattleCharacterData> characters;
    private void Start()
    {
        characters = new List<BattleCharacterData>();
    }
}
