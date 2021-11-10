using UnityEngine;
using UnityEngine.EventSystems;

public class StartSwap : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameObject[] swapSlots;
    [SerializeField] GameObject canvasToDeactivate;
    [SerializeField] GameObject canvasToActivate;
    public int slotOnArray;

    private void Awake()
    {
        swapSlots = GameObject.FindGameObjectsWithTag("Player Team List");
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        foreach (GameObject item in swapSlots)
        {
            item.GetComponent<SwapCharacterButton>().GiveSlotOnArray(slotOnArray);
            item.GetComponent<SwapCharacterButton>().GiveLineupSlot(this.gameObject);
        }
        canvasToDeactivate.SetActive(false);
        canvasToActivate.SetActive(true);
    }
}
