[System.Serializable]
public class RoomData
{
    public RoomSO so;
    public int goldGen;
    public int upgradeLvl;

    public void SetCurrents()
    {
        goldGen = so.baseGoldGeneration;
        upgradeLvl = 1;
    }
}