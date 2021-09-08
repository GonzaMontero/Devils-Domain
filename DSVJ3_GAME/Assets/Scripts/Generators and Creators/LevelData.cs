using UnityEngine;

[System.Serializable]
public class LevelData
{
    struct Level
    {
        int levelNumber;
        int enemyAmount;
        BattleCharacterSO[] enemyType;
        Vector2[] enemyPositions;
    }

    Level[] levelsLayout;
}
