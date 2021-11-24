using UnityEngine;

public class CHEATS : MonoBehaviour
{
    [SerializeField] Player player;

    #region Editor Things
#if UNITY_EDITOR//||true
    
    //Character Set Level 1
    [Tooltip("Press this to set the character in the lineup index to level 1")]
    [SerializeField] bool setLevel1Character = false;
    [SerializeField] int level1CharacterLineupIndex;

    //Add Gold
    [Tooltip("Press this to add as much gold as indicated")]
    [SerializeField] bool addGold = false;
    [SerializeField] int addGoldQuantity;

    [Tooltip("Press this to add as much gems as indicated")]
    [SerializeField] bool addGems = false;
    [SerializeField] int addGemsQuantity;

    //Unity Events
    private void OnValidate() //You can have multiple booleans here
    {
        //SET CHARACTER LEVEL 1
        if (setLevel1Character)
        {
            // Your function here
            player.lineup[level1CharacterLineupIndex] = SetLevel1Character(player.lineup[level1CharacterLineupIndex]);

            //When its done set this bool to false
            //This is useful if you want to do some stuff only when clicking this "button"
            setLevel1Character = false;
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
    }

    //Methods
    BattleCharacterData SetLevel1Character(BattleCharacterData characterToSet)
    {
        if (!characterToSet.so) return null;

        return new BattleCharacterData(characterToSet.so);
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
}