using UnityEngine;

public class CHEATS : MonoBehaviour
{
    [SerializeField] Player player;

    private void Start()
    {
        player = Player.Get();
    }

    #region Editor Things
#if UNITY_EDITOR

    [Header("Character Vars")]
    [SerializeField] EnemyHolder levelSO;
    [SerializeField] BattleCharacterSO characterSO;
    [SerializeField] int characterIndex;

    [Header("Character Level Vars")]
    [SerializeField] int characterLevels;

    [Header("Currency Vars")]
    [SerializeField] int addCurrencyQuantity;

    [Header("Set Character Level - ONLY RUNTIME")]
    [Tooltip("Press this to set the character in the lineup index to level X")]
    [SerializeField] bool setLeveledCharacter = false;

    [Header("Add Character to Characters Pool")]
    [Tooltip("Press this to add the character to the player's characters array in index")]
    [SerializeField] bool addCharacterToPool = false;

    [Header("Add Character to Lineup")]
    [Tooltip("Press this to add the character to the player's lineup in index")]
    [SerializeField] bool addCharacterToLineup = false;

    [Header("Add Character to Level")]
    [Tooltip("Press this to add the character to the level in index")]
    [SerializeField] bool addCharacterToLevel = false;

    [Header("Add Gold")]
    [Tooltip("Press this to add as much gold as indicated")]
    [SerializeField] bool addGold = false;

    [Header("Add Gems")]
    [Tooltip("Press this to add as much gems as indicated")]
    [SerializeField] bool addGems = false;

    [Header("Delete Player Data")]
    [Tooltip("Press this to delete player data - WARNING, DATA IS LOST FOREVER")]
    [SerializeField] bool deleteData = false;

    //Unity Events
    private void OnValidate() //You can have multiple booleans here
    {
        //SET CHARACTER LEVEL 1
        if (setLeveledCharacter)
        {
            // Your function here
            CharacterData data = SetCharacterLevel(player.lineup[characterIndex], characterLevels);
            player.lineup[characterIndex] = data;

            //When its done set this bool to false
            //This is useful if you want to do some stuff only when clicking this "button"
            setLeveledCharacter = false;
            return;
        }

        //ADD CHARACTER TO POOL
        if (addCharacterToPool)
        {
            // Your function here
            AddCharacterToPool();

            //When its done set this bool to false
            //This is useful if you want to do some stuff only when clicking this "button"
            addCharacterToPool = false;
            return;
        }

        //ADD CHARACTER TO LINEUP
        if (addCharacterToLineup)
        {
            // Your function here
            AddCharacterToLineup();

            //When its done set this bool to false
            //This is useful if you want to do some stuff only when clicking this "button"
            addCharacterToLineup = false;
            return;
        }

        //ADD CHARACTER TO LEVEL
        if (addCharacterToLevel)
        {
            // Your function here
            AddCharacterToLevel();

            //When its done set this bool to false
            //This is useful if you want to do some stuff only when clicking this "button"
            addCharacterToLevel = false;
            return;
        }

        //ADD X GOLD
        if (addGold)
        {
            // Your function here
            AddGold(addCurrencyQuantity);

            //When its done set this bool to false
            //This is useful if you want to do some stuff only when clicking this "button"
            addGold = false;
            return;
        }

        //ADD X GEMS
        if (addGems)
        {
            // Your function here
            AddGems(addCurrencyQuantity);

            //When its done set this bool to false
            //This is useful if you want to do some stuff only when clicking this "button"
            addGems = false;
            return;
        }

        //DELETE PLAYER DATA
        if (deleteData)
        {
            // Your function here
            player.DeleteData();

            //When its done set this bool to false
            //This is useful if you want to do some stuff only when clicking this "button"
            deleteData = false;
            return;
        }
    }

    //Methods
    CharacterData SetCharacterLevel(CharacterData characterToSet, int levels)
    {
        if (characterToSet.indexSO < 0) return null;
        CharacterData newCharacter = new CharacterData(characterToSet.so);
        for (int i = 1; i < levels; i++)
        {
            newCharacter.LevelUp();
            newCharacter.UpdateXpRequisites();
        }
        return newCharacter;
    }
    void AddCharacterToPool()
    {
        if (characterIndex < 0) return;
        player.characters[characterIndex].so = characterSO;
    }
    void AddCharacterToLineup()
    {
        if (characterIndex < 0 || characterIndex > 5) return;
        player.lineup[characterIndex].so = characterSO;
    }
    void AddCharacterToLevel()
    {
        if (characterIndex < 0 || characterIndex >= levelSO.enemies.Length) return;
        levelSO.enemies[characterIndex].enemy.so = characterSO;
    }
#endif
    #endregion

    public void AddGold(int gold)
    {
        player.gold += gold;
    }
    public void AddGems(int gems)
    {
        player.gems += gems;
    }
    public void SetPlayerLevel(int level)
    {
        if (level < 0) level = 0;
        player.level = level;
    }
}