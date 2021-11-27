using System;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public Action RoomDestroy;
    public Action RoomUpdate;
    [SerializeField] RoomData data;

    public RoomData GetData()
    {
        return data;
    }
    public void LoadData(RoomData newData)
    {
        data = newData;
        RoomUpdate?.Invoke();
    }
    public void Build(RoomSO so)
    {
        //Set Data
        data = new RoomData();
        data.so = so;
        data.SetCurrents();
        RoomUpdate?.Invoke();
    }
    public void Destroy()
    {
        RoomDestroy?.Invoke();
        Destroy(gameObject);
    }
    public int GetUpgradeCost()
    {
        if (data.upgradeLvl >= data.so.maxUpgrades) { return -1; }
        return (int)(data.so.baseCost * data.so.updgradeCostMod * data.upgradeLvl * data.positionCostModifier);
    }
    public int GetBuildCost()
    {
        return data.so.baseCost * data.positionCostModifier;
    }
    public void Upgrade()
    {
        if (data.upgradeLvl >= data.so.maxUpgrades) { return; }

        data.upgradeLvl++;
        data.gemGen = data.so.baseGemGeneration * (2 * data.upgradeLvl);
        RoomUpdate?.Invoke();
    }
    public int GetGemGen()
    {
        return data.gemGen;
    }
}