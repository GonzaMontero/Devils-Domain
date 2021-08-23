using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldUI; //TEMP VARIABLE, JUST FOR PROTO
    [SerializeField] TextMeshProUGUI upgradeUI; //TEMP VARIABLE, JUST FOR PROTO
    [SerializeField] RoomManager roomManager;
    [SerializeField] PlayerManager player;
    [SerializeField] CameraController camera;

    private void Start()
    {
        roomManager.NotEnoughGold += OnNotEnoughGoldForUpgrade;
        roomManager.RoomClicked += OnRoomSelected;
        //roomManager.RoomUpdated += OnRoomUpdate;
        player.GoldUpdated += OnGoldUpdated;
        camera.ZoomingOut += OnZoomOut;
    }

    void OnRoomUpdate(int newUpgradeCost)
    {
        upgradeUI.text = "Upgrade\nCost: " + newUpgradeCost;
    }
    void OnGoldUpdated(int currentGold)
    {
        goldUI.text = "Gold: " + currentGold;
    }
    void OnNotEnoughGoldForUpgrade()
    {
        //activate warning
    }
    void OnRoomSelected(RoomController roomController)
    {
        int upgradeCost = roomController.GetUpgradeCost();
        upgradeUI.text = "Upgrade\nCost: " + upgradeCost;
        upgradeUI.transform.parent.gameObject.SetActive(true); //set button true
    }
    void OnZoomOut()
    {
        upgradeUI.transform.parent.gameObject.SetActive(false); //set button false
    }
}
