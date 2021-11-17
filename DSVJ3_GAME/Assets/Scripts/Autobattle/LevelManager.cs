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

    private void Start()
    {
        enemyP.SetCharacters(levels[currentLevel].enemies);
    }
}
