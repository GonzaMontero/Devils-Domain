using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI upgradeText;
    [SerializeField] TextMeshProUGUI buildText;
    [SerializeField] Canvas roomUIHolder;
    [SerializeField] RoomManager roomManager;
    [SerializeField] PlayerManager player;
    [SerializeField] CameraController mainCamera;

    private void Start()
    {
        roomManager.NotEnoughGold += OnNotEnoughGoldForUpgrade;
        roomManager.RoomClicked += OnRoomSelected;
        roomManager.RoomUpdated += OnRoomUpdate;
        roomManager.RoomClickable += MouseIsNotOverElement;
        player.GoldChanged += OnGoldUpdated;
        mainCamera.ZoomingOut += OnZoomOut;
    }

    bool MouseIsNotOverElement()
    {
        return !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }
    void OnRoomUpdate(int newUpgradeCost)
    {
        bool roomJustBuilded = buildText.transform.parent.gameObject.activeSelf;
        if (roomJustBuilded)
        {
            buildText.transform.parent.gameObject.SetActive(false); //set button false
            upgradeText.transform.parent.gameObject.SetActive(true); //set button true
        }

        if (newUpgradeCost == -1)
        {
            upgradeText.text = "MAX UPGRADE\nREACHED";
            return;
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
        roomUIHolder.enabled = false; //set room data invisible

        if (roomController.GetUpgradeCost() == -1)
        {
            buildText.text = "Build\nCost: " + buildCost; //TEMP?, REPLACE FOR ACTION?
            buildText.transform.parent.gameObject.SetActive(true); //set button true
            upgradeText.transform.parent.gameObject.SetActive(false); //set button from previous room false
            return;
        }

        upgradeText.text = "Upgrade\nCost: " + roomController.GetUpgradeCost(); //TEMP?, REPLACE FOR ACTION?
        upgradeText.transform.parent.gameObject.SetActive(true); //set button true
    }
    void OnZoomOut()
    {
        roomUIHolder.enabled = true; //set room data visible
        buildText.transform.parent.gameObject.SetActive(false); //set button false
        upgradeText.transform.parent.gameObject.SetActive(false); //set button false
    }
}