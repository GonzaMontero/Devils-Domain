using UnityEngine;

public class StartSwap : MonoBehaviour
{
    [SerializeField] GameObject[] swapSlots;
    [SerializeField] GameObject canvasToDeactivate;
    [SerializeField] GameObject canvasToActivate;
    public int slotOnArray;

    private void Awake()
    {
        swapSlots = GameObject.FindGameObjectsWithTag("Player Team List");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (GameObject item in swapSlots)
            {
                item.GetComponent<SwapCharacterButton>().GiveSlotOnArray(slotOnArray - 1);
                item.GetComponent<SwapCharacterButton>().GiveLineupSlot(this.gameObject);
            }
            canvasToDeactivate.SetActive(false);
            canvasToActivate.SetActive(true);
        }
    }
}
