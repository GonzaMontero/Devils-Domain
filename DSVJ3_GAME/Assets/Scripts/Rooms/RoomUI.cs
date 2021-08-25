using UnityEngine;
using TMPro;

public class RoomUI : MonoBehaviour
{
    public RoomController controller;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI costText;
    Transform roomUI;

    void Start()
    {
        //controller = gameObject.GetComponent<RoomController>();
        controller.RoomUpdate += OnRoomUpdate;
        //roomUI = Instantiate(Resources.Load<GameObject>("Prefabs/Room X-X")).transform;
        //roomUI.parent = GameObject.FindGameObjectWithTag("RoomsUIHolder").transform;
    }

    void OnRoomUpdate()
    {
        goldText.text = "Gold: " + controller.GetGoldGen().ToString();
        costText.text = "Cost: " + controller.GetUpgradeCost().ToString();
    }
}