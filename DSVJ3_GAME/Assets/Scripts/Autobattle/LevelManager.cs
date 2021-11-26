using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LevelSO", menuName = "Level Scriptable Object")]
public class EnemyHolder : ScriptableObject
{
    [Serializable]
    public struct EnemyInLevel
    {
        public BattleCharacterData enemy;
        public int positionInLevel;
    }

    public EnemyInLevel[] enemies;
}

public class LevelManager : MonoBehaviour
{
    [SerializeField] EnemyHolder[] levels;

    [SerializeField] EnemyParty enemyP;
    [SerializeField] PlayerParty playerP;

    int currentLevel;
    Player player;

    private void Start()
    {
        player = Player.Get();

        currentLevel = player.level;

        if (currentLevel < levels.Length)
        {
            enemyP.SetCharacters(levels[currentLevel].enemies);
        }
        else
        {
            enemyP.GenerateEnemies(GetLowestLevel(), GetHighestLevel());
        }
    }

    public void ResetLevel()
    {
        currentLevel = player.level;

        if (currentLevel < levels.Length)
        {
            enemyP.SetCharacters(levels[currentLevel].enemies);
        }
        else
        {
            enemyP.GenerateEnemies(GetLowestLevel(), GetHighestLevel());
        }
    }

    public int GetLowestLevel()
    {
        
        int lowestLevel = int.MaxValue;

        for (short i = 0; i < player.lineup.Length; i++)
        {
            if (player.lineup[i].level < lowestLevel)
            {
                lowestLevel = player.lineup[i].level;
            }
        }
        return lowestLevel;
    }
    public int GetHighestLevel()
    {
        int highestLevel = int.MinValue;

        for (short i = 0; i < player.lineup.Length; i++)
        {
            if (player.lineup[i].level > highestLevel)
            {
                highestLevel = player.lineup[i].level;
            }
        }
        return highestLevel;
    }
}
