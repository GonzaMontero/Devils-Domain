using UnityEngine;

public class CHEATS : MonoBehaviour
{
    [SerializeField] Player player;

#if UNITY_EDITOR//||true
    [Tooltip("Press this to set the character in the lineup index to level 1")]
    [SerializeField] bool setLevel1Character = false;
    [SerializeField] int level1CharacterLineupIndex;

    //Unity Events
    private void OnValidate()
    {
        //You can have multiple booleans here
        if (setLevel1Character)
        {
            // Your function here
            player.lineup[level1CharacterLineupIndex] = SetLevel1Character(player.lineup[level1CharacterLineupIndex]);

            //When its done set this bool to false
            //This is useful if you want to do some stuff only when clicking this "button"
            setLevel1Character = false;
        }
    }

    //Methods
    BattleCharacterData SetLevel1Character(BattleCharacterData characterToSet)
    {
        if (!characterToSet.so) return null;

        return new BattleCharacterData(characterToSet.so);
    }
#endif
}