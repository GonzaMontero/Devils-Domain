using UnityEngine;

//public enum CharaterType {  };
public enum AttackType { melee, both, ranged };

[CreateAssetMenu(fileName = "CharacterSO", menuName = "Character Scriptable Object")]
public class BattleCharacterSO : ScriptableObject
{
    [Header("Meta Variables")]
    public Sprite sprite;

    [Header("In-game Variables")]
    public Stats baseStats;
    public int baseXpToLevelUp;
    [Tooltip("Used to calculate xp required to lvl up - EXPLAIN BETTER LATER")]
    public int xpToLevelUpModifier;
    //public CharaterType type;
    public AttackType attackType;
}