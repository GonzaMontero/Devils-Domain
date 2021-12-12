[System.Serializable]
public class CharacterData
{
    public BattleCharacterSO so
    {
        get { if (indexSO < 0) return null; return CharacterDataManager.Get().characters[indexSO]; }
        set { if (value == null) { indexSO = -1; return; } indexSO = value.index; }
    }
    public int indexSO = -1;
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
    const float assassinSpeedMod = 0.25f;

    public CharacterData(BattleCharacterSO newSO, int level)
    {
        indexSO = newSO.index;
        SetLevel1Currents();

        for (int i = 1; i < level; i++)
        {
            LevelUp();
        }
    }
    public CharacterData(BattleCharacterSO newSO)
    {
        indexSO = newSO.index;
        SetLevel1Currents();
    }
    public void SetLevel1Currents()
    {
        if (indexSO < 0) return; //If there is no so (no character template), return

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
        level++;
        switch (so.attackType)
        {
            //case AttackType.melee:
            //    currentStats.maxHealth = so.baseStats.maxHealth + ((float)level * so.baseXpToLevelUp * meleeMaxHealthMod);
            //    currentStats.armor = (float)level * so.baseStats.armor * meleeArmor;
            //    break;
            //case AttackType.ranged:
            //    currentStats.maxHealth = (int)((float)level * so.baseXpToLevelUp * so.baseStats.maxHealth * rangedMaxHealthMod);
            //    currentStats.damage = (int)((float)level * so.baseXpToLevelUp * so.baseStats.damage * rangedDamageMod);
            //    break;
            //case AttackType.assasin:
            //    currentStats.attackSpeed = (float)level * so.baseXpToLevelUp * so.baseStats.attackSpeed * assassinSpeedMod;
            //    currentStats.damage = (int)((float)level * so.baseXpToLevelUp * so.baseStats.damage * assassinDamageMod);
            //    break;
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
        SetStartOfBattleCurrents();
    }
    public void UpdateXpRequisites()
    {
        currentXpToLevelUp = so.xpToLevelUpModifier * level;
    }
}