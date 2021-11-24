[System.Serializable]
public class RoomData
{
    public RoomSO so;
    public int gemGen;
    public int upgradeLvl;
    public int positionCostModifier = 1; //the cost modifier affected by pos of first room

    public void SetCurrents()
    {
        gemGen = so.baseGemGeneration;
        upgradeLvl = 1;
    }
}