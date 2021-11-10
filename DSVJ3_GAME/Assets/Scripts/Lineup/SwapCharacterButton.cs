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
        if(player.characters.Count > slotOnList)
        {
            if(player.characters[slotOnList] != null)
            {
                lineupSlot.transform.GetChild(0).GetComponent<Image>().sprite = player.characters[slotOnList].so.lineupFaceSprite;
                player.SwapPositions(indexSlot, player.characters[slotOnList]);
                canvasToDeactivate.SetActive(false);
                canvasToActivate.SetActive(true);
            }
        }
        else
        {
            canvasToDeactivate.SetActive(false);
            canvasToActivate.SetActive(true);
        }
        
    }

    private void OnDisable()
    {
        if (player.characters.Count > slotOnList)
        {
            this.transform.GetChild(0).GetComponent<Image>().sprite = player.characters[slotOnList].so.lineupFaceSprite;
        }
        else
        {
            this.transform.GetChild(0).GetComponent<Image>().sprite = emptyFace;
        }
    }
}
