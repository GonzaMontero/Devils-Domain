using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI upgradeText; 
    [SerializeField] TextMeshProUGUI buildText; 
    [SerializeField] RoomManager roomManager;
    [SerializeField] PlayerManager player;
    [SerializeField] CameraController mainCamera;

    private void Start()
    {
        roomManager.NotEnoughGold += OnNotEnoughGoldForUpgrade;
        roomManager.RoomClicked += OnRoomSelected;
        roomManager.RoomUpdated += OnRoomUpdate;
        player.GoldUpdated += OnGoldUpdated;
        mainCamera.ZoomingOut += OnZoomOut;
    }

    void OnRoomUpdate(int newUpgradeCost)
    {
        bool roomJustBuilded = buildText.transform.parent.gameObject.activeSelf;
        if (roomJustBuilded)
        {
            buildText.transform.parent.gameObject.SetActive(false); //set button false
            upgradeText.transform.parent.gameObject.SetActive(true); //set button true
        }

        upgradeText.text = "Upgrade\nCost: " + newUpgradeCost;
    }
    void OnGoldUpdated(int currentGold)
    {
        goldText.text = "Gold: " + currentGold;
    }
    void OnNotEnoughGoldForUpgrade()
    {
        //activate warning
    }
    void OnRoomSelected(RoomController roomController, int buildCost)
    {
        if (roomController.GetUpgradeCost() == -1)
        {
            buildText.text = "Build\nCost: " + buildCost; //TEMP?, REPLACE FOR ACTION?
            buildText.transform.parent.gameObject.SetActive(true); //set button true
            return;
        }
        upgradeText.text = "Upgrade\nCost: " + roomController.GetUpgradeCost(); //TEMP?, REPLACE FOR ACTION?
        upgradeText.transform.parent.gameObject.SetActive(true); //set button true
    }
    void OnZoomOut()
    {
        buildText.transform.parent.gameObject.SetActive(false); //set button false
        upgradeText.transform.parent.gameObject.SetActive(false); //set button false
    }
}
