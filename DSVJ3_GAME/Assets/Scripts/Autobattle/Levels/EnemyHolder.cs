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