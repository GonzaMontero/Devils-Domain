using System.Collections.Generic;
using UnityEngine;

public class EnemyParty : PartyManager
{
    [SerializeField] GameObject characterPrefab;
    [SerializeField] bool generateRandomEnemies = true;
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
        if (!generateRandomEnemies) return;
        
        //Set Path according to the correct tier;
        SetSOsPath();

        //Get character SOs to generate enemies
        characterSOs = Resources.LoadAll<BattleCharacterSO>(SOsPath);

        GenerateEnemies();
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

        //Generate new one
        GenerateEnemies();
    }
    void SetSOsPath()
    {
        SOsPath = "Scriptable Objects/Characters/Allies/" + charactersTier + " Star";
    }
    void GenerateEnemies()
    {
        for (short i = 0; i < 5; i++)
        {
            GenerateRandomEnemy();
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