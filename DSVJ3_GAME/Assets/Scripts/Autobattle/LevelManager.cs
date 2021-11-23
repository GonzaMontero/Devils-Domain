using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemyHolder
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
            enemyP.GenerateEnemies();
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
            enemyP.GenerateEnemies();
        }
    }
}
