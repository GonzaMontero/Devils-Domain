[System.Serializable]
public class BattleCharacterData
{
    public BattleCharacterSO so;
    public Stats currentStats;
    public int health;
    public int currentXP;
    public int currentXpToLevelUp;
    public int level;
    public int merge;

    //Level up values
    const float meleeMaxHealthMod = 1;
    const float meleeArmor = 0.25f;
    const float rangedMaxHealthMod = (float)1/5;
    const float rangedDamageMod = 0.5f;
    const float assassinDamageMod = (float)1/20;
    const float assassinSpeedMod = (float)1/5;

    public BattleCharacterData(BattleCharacterSO newSO, int level)
    {
        so = newSO;
        SetLevel1Currents();

        for (int i = 1; i < level; i++)
        {
            LevelUp();
        }
    }
    public BattleCharacterData(BattleCharacterSO newSO)
    {
        so = newSO;
        SetLevel1Currents();
    }
    public void SetLevel1Currents()
    {
        if (!so) return; //If there is no so (no character template), return

        currentStats = so.baseStats;
        currentXP = 0;
        currentXpToLevelUp = so.baseXpToLevelUp;
        if (level < 1) level = 1;
        if (merge < 1) merge = 1;
    }
    public void SetStartOfBattleCurrents()
    {
        health = currentStats.maxHealth;
    }
    public void LevelUp()
    {
        switch (so.attackType)
        {
            case AttackType.melee:
                currentStats.maxHealth += (int)(so.baseXpToLevelUp * meleeMaxHealthMod);
                currentStats.armor += meleeArmor;
                break;
            case AttackType.ranged:
                currentStats.maxHealth += (int)(so.baseXpToLevelUp * rangedMaxHealthMod);
                currentStats.damage += (int)(so.baseXpToLevelUp * rangedDamageMod);
                break;
            case AttackType.assasin:
                currentStats.attackSpeed += so.baseXpToLevelUp * assassinSpeedMod;
                currentStats.damage += (int)(so.baseXpToLevelUp * assassinDamageMod);
                break;
            default:
                break;
        }
        level++;
        SetStartOfBattleCurrents();
    }
    public void UpdateXpRequisites()
    {
        currentXpToLevelUp = so.xpToLevelUpModifier * level;
    }
}