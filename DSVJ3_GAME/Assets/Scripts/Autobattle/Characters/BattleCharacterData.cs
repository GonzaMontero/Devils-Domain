[System.Serializable]
public class BattleCharacterData
{
    public BattleCharacterSO so;
    public Stats currentStats;
    public int health;
    public int currentXP;
    public int currentXpToLevelUp;
    public int merge;

    public void SetLevel1Currents()
    {
        if (!so)
            return;
        currentStats = so.baseStats;
        currentXP = 0;
        currentXpToLevelUp = so.baseXpToLevelUp;
        merge = 1;
    }
    public void SetStartOfBattleCurrents()
    {
        health = currentStats.maxHealth;
    }
}