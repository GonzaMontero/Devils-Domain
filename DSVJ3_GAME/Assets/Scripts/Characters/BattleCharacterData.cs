public class BattleCharacterData
{
    public BattleCharacterSO so;
    public Stats currentStats;
    public int health;
    public int currentXP;
    public int currentXpToLevelUp;
    public int merge;

    public void GetCurrents()
    {
        currentStats = so.baseStats;
        currentXP = 0;
        currentXpToLevelUp = so.baseXpToLevelUp;
        merge = 1;
    }
}