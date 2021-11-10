using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] List<BattleCharacterData[]> levels;
    [SerializeField] int[] levelInStage;

    [SerializeField] GameObject[] enemySlots;

    int currentLevel;

    private void Start()
    {
        BattleCharacterData[] levelEnemies = levels[currentLevel];

        for (short i = 0; i < enemySlots.Length; i++)
        {
            if (levelEnemies[i] != null)
            {
                enemySlots[i].GetComponent<BattleCharacterController>().SetData(levelEnemies[i]);
                enemySlots[i].GetComponent<BattleCharacterData>().level = levelInStage[currentLevel];                
            }
        }
    }
}
