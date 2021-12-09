using UnityEngine;
using TMPro;

public class UIRoom : MonoBehaviour
{
    public RoomController controller;
    [SerializeField] TextMeshProUGUI gemText;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] GameObject[] upgrades;
    Transform roomUI;

    void Start()
    {
        //Link Action
        //controller = gameObject.GetComponent<RoomController>();
        controller.RoomUpdate += OnRoomUpdate;
        //roomUI = Instantiate(Resources.Load<GameObject>("Prefabs/Room X-X")).transform;
        //roomUI.parent = GameObject.FindGameObjectWithTag("RoomsUIHolder").transform;

        //Set Data
        OnRoomUpdate();
    }

    void OnRoomUpdate()
    {
        if (controller.GetGemGen() <= 0) { return; }
        //gemText.text = "Gems: " + controller.GetGemGen().ToString();
        //costText.text = "Cost: " + controller.GetUpgradeCost().ToString();
        
        for (int i = 0; i < controller.GetData().upgradeLvl; i++)
        {
            if (i >= upgrades.Length) return;
            upgrades[i].SetActive(true);
        }
    }
}