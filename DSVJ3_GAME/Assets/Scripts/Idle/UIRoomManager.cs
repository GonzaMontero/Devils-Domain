using UnityEngine;
using TMPro;

public class UIRoomManager : MonoBehaviour
{
    [SerializeField] RoomManager roomManager;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI gemsText;
    [SerializeField] TextMeshProUGUI upgradeText;
    [SerializeField] TextMeshProUGUI buildText;
    [SerializeField] TextMeshProUGUI gemGenText;
    [SerializeField] float noGoldWarningDuration;
    Player player;

    //Unity Events
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

    //Methods
    void SetOffNotEnoughGoldWarning()
    {
        buildText.color = Color.black;
        upgradeText.color = Color.black;
        goldText.color = Color.white;
    }

    //Event Receivers
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
            gemGenText.text = "Gems per Minute\n" + player.room.gemGen;
            upgradeText.text = "Upgrade\nCost: " + newCost;
        }
    }
    void OnRoomUpdate(int newUpgradeCost)
    {
        buildText.transform.parent.gameObject.SetActive(false); //set button false
        upgradeText.transform.parent.gameObject.SetActive(true); //set button true
        gemGenText.text = "Gems per Minute\n" + player.room.gemGen;

        if (newUpgradeCost == -1)
        {
            upgradeText.text = "MAX UPGRADE\nREACHED";
            return;
        }

        upgradeText.text = "Upgrade\nCost: " + newUpgradeCost;
    }
    void OnGoldUpdated()
    {
        goldText.text = player.gold.ToString();
    }
    void OnGemsUpdated()
    {
        gemsText.text = player.gems.ToString();
    }
    void OnNotEnoughGoldForUpgrade()
    {
        buildText.color = Color.red;
        upgradeText.color = Color.red;
        goldText.color = Color.red;
        StopAllCoroutines();
        Invoke("SetOffNotEnoughGoldWarning", noGoldWarningDuration);
    }
}