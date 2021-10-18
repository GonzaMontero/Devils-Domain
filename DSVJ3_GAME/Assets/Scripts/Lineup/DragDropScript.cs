using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropScript : MonoBehaviour, IEndDragHandler, IDragHandler{

    private Vector3 startPos;
    private Vector3 newPos;

    private int positionOnCharacterCount;
    private Player player;
    private bool endDrag;

    private void Start()
    {
        player = Player.Get();
    }

    public void SetValues(Vector3 startPosSet, int positionOnCharacterCountD)
    {
        startPos = startPosSet;
        positionOnCharacterCount = positionOnCharacterCountD;
    }

    public void OnDrag(PointerEventData eventData)
    {
        newPos.x = Input.mousePosition.x;
        newPos.y = Input.mousePosition.y;
        newPos.z = 0;

        transform.position = newPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        endDrag = true;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Lineup"))
        {
            if (endDrag)
            {
                collision.gameObject.GetComponent<SwapLineupSpot>().swapPositionOnArray(player.characters[positionOnCharacterCount]);
                transform.position = startPos;
            }
            else
            {
                transform.position = startPos;
            }
        }
        else
        {
            transform.position = startPos;
        }
    }
}