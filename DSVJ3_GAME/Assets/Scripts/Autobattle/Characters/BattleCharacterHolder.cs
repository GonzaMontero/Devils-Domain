using System;
using UnityEngine;

public class BattleCharacterHolder : MonoBehaviour
{
    public Action<BoxCollider2D, BattleCharacterController> CharacterPositioned;
    public Action<BattleCharacterController> CharacterRemoved;
    [SerializeField] Transform top;
    [SerializeField] Transform bottom;
    Vector2 originalPosition;   
    Vector2 newPosition;
    LayerMask slotsMask;
    RaycastHit2D slotHitted;
    BoxCollider2D boxCollider;
    SpriteRenderer lastSlotSprite;
    GameObject boxCast;

    #region Unity Events
    private void Start()
    {
        slotsMask = LayerMask.GetMask("Slots");
        boxCollider = GetComponent<BoxCollider2D>();
        originalPosition = transform.position;
    }
    private void OnMouseDown()
    {
        newPosition = transform.position; //newPosition equal original position
        Debug.Log("Saved Original Pos!");
    }
    private void OnMouseDrag()
    {
        //Adapt mousePos to worldSpace
        Vector3 screenMouseAux = new Vector3();
        screenMouseAux.x = Input.mousePosition.x;
        screenMouseAux.y = Input.mousePosition.y;
        screenMouseAux.z = -Camera.main.transform.position.z;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(screenMouseAux);
        transform.position = mousePos;
        GetSlot();
        //Debug.Log("Dragged Pos! " + mousePos);
    }
    private void OnMouseUp()
    {
        GetSlotPos(); //check if there is a slot under character and return slotPos, if not, leave ogPos
        transform.position = newPosition;
        transform.localPosition = (Vector2)transform.localPosition; //set local Z to 0
        Debug.Log("Moved Pos To Target Pos!");
    }
    #endregion

    void GetSlot()
    {
        Vector2 pos = (Vector2)transform.position + boxCollider.size.y / 2 * Vector2.down;
        //Vector2 hitPos = pos + Vector2.up * 4;

        
        slotHitted = Physics2D.BoxCast(pos, Vector2.one, 0, Vector2.up, 0.1f, slotsMask);
        if (slotHitted && !slotHitted.transform.CompareTag("SlotTaken"))
        {
            Debug.Log("Collided");
            UpdateSlotColor(slotHitted);
        }
        else
        {
            Debug.Log("Not Collided");
        }
    }
    void GetSlotPos()
    {
        //Invoke Character Positioned Action
        BattleCharacterController character = GetComponent<BattleCharacterController>();

        if (slotHitted && !slotHitted.transform.CompareTag("SlotTaken"))
        {
            newPosition = slotHitted.transform.position;
            
            BoxCollider2D slotCollider = slotHitted.transform.GetComponent<BoxCollider2D>();
            CharacterPositioned?.Invoke(slotCollider, character);
        }
        else
        {
            newPosition = originalPosition;
            CharacterRemoved(character);
        }

        if (lastSlotSprite)
        {
            lastSlotSprite.color = Color.white; //character positioned, so color marking is not needed
        }
    }
    void UpdateSlotColor(RaycastHit2D slotHitted)
    {
        if (lastSlotSprite)
        {
            lastSlotSprite.color = Color.white;
        }
        lastSlotSprite = slotHitted.transform.GetComponent<SpriteRenderer>();
        lastSlotSprite.color = Color.red;
    }
}