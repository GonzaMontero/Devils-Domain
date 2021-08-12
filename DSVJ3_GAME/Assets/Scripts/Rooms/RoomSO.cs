using UnityEngine;

[CreateAssetMenu(fileName = "RoomSO", menuName = "Room Scriptable Object")]
public class RoomSO : ScriptableObject
{
    [Header("Game Object")]
    public float height;
    public float width;
    public Sprite sprite;

    [Header("Room Design")]
    public int maxUpgrades;
    public int baseGoldGeneration;
    public int baseCost;
    [Tooltip("Defines cost of each upgrade, multiplies base cost by this to the power of upgrade")]
    public float updgradeCostMod;
}