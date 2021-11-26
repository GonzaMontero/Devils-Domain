using System.Collections.Generic;
using UnityEngine;

public class EnemyParty : PartyManager
{
    [SerializeField] GameObject characterPrefab;
    public bool generateRandomEnemies = true;
    [SerializeField] int charactersTierSetter = 3;
    BattleCharacterSO[] characterSOs;
    string SOsPath;
    int charactersTier 
    {
        get
        { 
            if (charactersTierSetter < 3) return 3; 
            else if (charactersTierSetter > 5) return 5; 
            else return charactersTierSetter;
        }
    }

    //Unity Events
    private void Start()
    {                
        //Set Path according to the correct tier;
        SetSOsPath();

        //Get character SOs to generate enemies
        characterSOs = Resources.LoadAll<BattleCharacterSO>(SOsPath);

        //if (!generateRandomEnemies) return;
        //GenerateEnemies();
    }
    private void GetSOs()
    {
        //Set Path according to the correct tier;
        SetSOsPath();

        //Get character SOs to generate enemies
        characterSOs = Resources.LoadAll<BattleCharacterSO>(SOsPath);
    }
    //Methods
    public override void ResetParty()
    {
        readyToBattle = false;

        //Remove old party
        BattleCharacterController characterToRemove;
        while (characters.Count > 0)
        {
            characterToRemove = characters[0];
            RemoveCharacter(characters[0]);
            Destroy(characterToRemove.gameObject);
        }
    }
    void SetSOsPath()
    {
        SOsPath = "Scriptable Objects/Characters/Allies/" + charactersTier + " Star";
    }
    public void GenerateEnemies(int minLevel, int maxLevel)
    {
        if (characterSOs == null)
        {
            GetSOs();
        }
        for (short i = 0; i < 5; i++)
        {
            int randomLevel;
            if (minLevel > 3)
            {
                randomLevel = UnityEngine.Random.Range(minLevel - 2, maxLevel + 1);
            }
            else
            {
                randomLevel = UnityEngine.Random.Range(minLevel, maxLevel + 1);
            }
            
            GenerateRandomEnemy();
            for(short j = 0; j < randomLevel; j++)
            {
                characters[i].ReceiveXP(characters[i].publicData.currentXpToLevelUp+1);
            }
        }
    }
    void GenerateEnemy(BattleCharacterData data)
    {       
        int tileIndex = Random.Range(0, characterTiles.Length);

        while (characterInTile.ContainsKey(tileIndex))
        {
            tileIndex = Random.Range(0, characterTiles.Length);
        }

        BattleCharacterController character;
        character = Instantiate(characterPrefab, transform).GetComponent<BattleCharacterController>();
        character.transform.position = characterTiles[tileIndex].transform.position;
        character.GetComponent<SpriteRenderer>().flipX = true;
        character.GetComponent<SpriteRenderer>().sortingOrder = -(int)character.transform.position.z;

        character.SetData(data);
        AddCharacter(character, tileIndex);
    }
    void GenerateEnemy(BattleCharacterData data, int tileIndex)
    {
        BattleCharacterController character;
        character = Instantiate(characterPrefab, transform).GetComponent<BattleCharacterController>();
        character.transform.position = characterTiles[tileIndex].transform.position;
        character.GetComponent<SpriteRenderer>().flipX = true;
        character.GetComponent<SpriteRenderer>().sortingOrder = -(int)character.transform.position.z;

        character.SetData(data);
        for(int i=1; i < data.level;i++)
        {
            character.publicData.LevelUp();
        }
        AddCharacter(character, tileIndex);
    }
    void GenerateRandomEnemy()
    {
        int soIndex = Random.Range(0, characterSOs.Length);
        BattleCharacterData data = new BattleCharacterData(characterSOs[soIndex]);
        GenerateEnemy(data);
    }
    public void SetCharacters(EnemyHolder.EnemyInLevel[] enemies)
    {
        foreach(EnemyHolder.EnemyInLevel enemy in enemies)
        {
            enemy.enemy.SetLevel1Currents();
            GenerateEnemy(enemy.enemy, enemy.positionInLevel);
        }        
    }
}