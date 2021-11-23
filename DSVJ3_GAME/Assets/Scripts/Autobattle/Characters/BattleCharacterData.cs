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
        if (!so)
            return;
        currentStats = so.baseStats;
        currentXP = 0;
        currentXpToLevelUp = so.baseXpToLevelUp;
        if (level < 1)      level = 1;
        if (merge < 1)      merge = 1;
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
                currentStats.maxHealth += so.baseXpToLevelUp;
                currentStats.armor += 0.25f;
                break;
            case AttackType.assasin:
                currentStats.attackSpeed += so.baseXpToLevelUp / 20;
                currentStats.damage += so.baseXpToLevelUp / 5;
                break;
            case AttackType.ranged:
                currentStats.damage += so.baseXpToLevelUp / 2;
                currentStats.maxHealth += so.baseXpToLevelUp / 5;
                break;
            default:
                break;
        }
        SetStartOfBattleCurrents();
    }
}