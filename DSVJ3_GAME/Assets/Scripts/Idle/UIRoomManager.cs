using UnityEngine;
using TMPro;

public class UIRoomManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI gemsText;
    [SerializeField] TextMeshProUGUI upgradeText;
    [SerializeField] TextMeshProUGUI buildText;
    [SerializeField] RoomManager roomManager;
    Player player;

    private void Start()
    {
        //Get Player
        player = Player.Get();
        //Link Actions
        player.GoldChanged += OnGoldUpdated;
        player.GemsChanged += OnGemsUpdated;
        roomManager.NotEnoughGold += OnNotEnoughGoldForUpgrade;
        roomManager.RoomLoad += OnRoomInit;
        roomManager.RoomUpdate += OnRoomUpdate;

        //Set UI values
        OnGoldUpdated();
        OnGemsUpdated();
    }

    void OnRoomInit(int newCost, bool roomBuilded)
    {
        if (!roomBuilded)
        {
            buildText.transform.parent.gameObject.SetActive(true); //set button false
            upgradeText.transform.parent.gameObject.SetActive(false); //set button true
            buildText.text = "Build\nCost: " + newCost;
        }
        else
        {
            buildText.transform.parent.gameObject.SetActive(false); //set button false
            upgradeText.transform.parent.gameObject.SetActive(true); //set button true
            upgradeText.text = "Upgrade\nCost: " + newCost;
        }
    }
    void OnRoomUpdate(int newUpgradeCost)
    {
        buildText.transform.parent.gameObject.SetActive(false); //set button false
        upgradeText.transform.parent.gameObject.SetActive(true); //set button true

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
}