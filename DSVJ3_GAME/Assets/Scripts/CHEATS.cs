using UnityEngine;

public class CHEATS : MonoBehaviour
{
    [SerializeField] Player player;

    private void Start()
    {
        player = Player.Get();
    }

    #region Editor Things
#if UNITY_EDITOR//||true

    [Header("Set Character Level")]
    [Tooltip("Press this to set the character in the lineup index to level X")]
    [SerializeField] bool setLeveledCharacter = false;
    [SerializeField] int leveledCharacterLineupIndex;
    [SerializeField] int characterLevels;

    [Header("Add Gold")]    
    [Tooltip("Press this to add as much gold as indicated")]
    [SerializeField] bool addGold = false;
    [SerializeField] int addGoldQuantity;

    [Header("Add Gems")]
    [Tooltip("Press this to add as much gems as indicated")]
    [SerializeField] bool addGems = false;
    [SerializeField] int addGemsQuantity;

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
            BattleCharacterData data = SetCharacterLevel(player.lineup[leveledCharacterLineupIndex], characterLevels);
            player.lineup[leveledCharacterLineupIndex] = data;

            //When its done set this bool to false
            //This is useful if you want to do some stuff only when clicking this "button"
            setLeveledCharacter = false;
            return;
        }

        //ADD X GOLD
        if (addGold)
        {
            // Your function here
            AddGold(addGoldQuantity);

            //When its done set this bool to false
            //This is useful if you want to do some stuff only when clicking this "button"
            addGold = false;
            return;
        }

        //ADD X GEMS
        if (addGems)
        {
            // Your function here
            AddGems(addGemsQuantity);

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
    BattleCharacterData SetCharacterLevel(BattleCharacterData characterToSet, int levels)
    {
        if (!characterToSet.so) return null;
        BattleCharacterData newCharacter = new BattleCharacterData(characterToSet.so);
        for (int i = 1; i < levels; i++)
        {
            newCharacter.LevelUp();
            newCharacter.UpdateXpRequisites();
        }
        return newCharacter;
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