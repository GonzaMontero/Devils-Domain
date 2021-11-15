﻿using UnityEngine;
using TMPro;

public class UIRoomsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI gemsText;
    [SerializeField] TextMeshProUGUI upgradeText;
    [SerializeField] TextMeshProUGUI buildText;
    [SerializeField] Canvas roomUIHolder;
    [SerializeField] RoomManager roomManager;
    [SerializeField] CameraController mainCamera;
    Player player;

    private void Start()
    {
        //Get Player
        player = Player.Get();
        //Link Actions
        player.GoldChanged += OnGoldUpdated;
        player.GemsChanged += OnGemsUpdated;
        roomManager.NotEnoughGold += OnNotEnoughGoldForUpgrade;
        roomManager.RoomClicked += OnRoomSelected;
        roomManager.RoomUpdated += OnRoomUpdate;
        //roomManager.RoomClickable += MouseIsNotOverElement;
        mainCamera.ZoomingOut += OnZoomOut;

        //Set UI values
        OnGoldUpdated();
        OnGemsUpdated();
    }

    //bool MouseIsNotOverElement()
    //{
    //    return !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    //}
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
    void OnGoldUpdated()
    {
        goldText.text = "Gold: " + player.gold;
    }
    void OnGemsUpdated()
    {
        gemsText.text = "Gems: " + player.gems;
    }
    void OnNotEnoughGoldForUpgrade()
    {
        //activate warning
    }
    void OnRoomSelected(RoomController roomController)
    {
        roomUIHolder.enabled = false; //set room data invisible

        if (roomController.GetUpgradeCost() == -1)
        {
            buildText.text = "Build\nCost: " + roomController.GetBuildCost();
            buildText.transform.parent.gameObject.SetActive(true); //set button true
            upgradeText.transform.parent.gameObject.SetActive(false); //set button from previous room false
            return;
        }

        upgradeText.text = "Upgrade\nCost: " + roomController.GetUpgradeCost();
        buildText.transform.parent.gameObject.SetActive(false); //set button from previous room false
        upgradeText.transform.parent.gameObject.SetActive(true); //set button true
    }
    void OnZoomOut()
    {
        roomUIHolder.enabled = true; //set room data visible
        buildText.transform.parent.gameObject.SetActive(false); //set button false
        upgradeText.transform.parent.gameObject.SetActive(false); //set button false
    }
}