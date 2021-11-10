using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwapCharacterButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameObject canvasToDeactivate;
    [SerializeField] GameObject canvasToActivate;
    [SerializeField] Sprite emptyFace;
    Player player;
    int indexSlot;
    int slotOnList;
    GameObject lineupSlot;

    private void Awake()
    {
        player = Player.Get();
    }

    public void GiveSlotOnList(int slotOn)
    {
        slotOnList = slotOn;
    }

    public void GiveSlotOnArray(int slotOn)
    {
        indexSlot = slotOn;
    }

    public void GiveLineupSlot(GameObject gm)
    {
        lineupSlot = gm;
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (player.lineup[indexSlot].so != null)
        {
            this.GetComponentInChildren<Image>().sprite = player.lineup[indexSlot].so.lineupFaceSprite;
        }
        else
        {
            this.GetComponentInChildren<Image>().sprite = emptyFace;
        }
        lineupSlot.GetComponentInChildren<Image>().sprite = player.characters[slotOnList].so.lineupFaceSprite;
        player.SwapPositions(indexSlot, player.characters[slotOnList]);
        canvasToDeactivate.SetActive(false);
        canvasToActivate.SetActive(true);
    }
}
