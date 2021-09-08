using System;
using UnityEngine;

public class BattleCharacterHolder : MonoBehaviour
{
    public Action<BoxCollider2D, BattleCharacterController> CharacterPositioned;
    Vector2 originalPosition;
    Vector2 newPosition;
    LayerMask slotsMask;
    BoxCollider2D boxCollider;

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

    #region Methods
    void GetSlotPos()
    {
        Vector2 pos = transform.position;
        RaycastHit2D slotHitted;
        slotHitted = Physics2D.BoxCast(pos, boxCollider.size, 0, Vector2.up, 0, slotsMask);
        if (slotHitted)
        {
            newPosition = slotHitted.transform.position;

            //Invoke Character Positioned Action
            BattleCharacterController character = GetComponent<BattleCharacterController>();
            BoxCollider2D slotCollider = slotHitted.transform.GetComponent<BoxCollider2D>();
            CharacterPositioned?.Invoke(slotCollider, character);
        }
        else
        {
            newPosition = originalPosition;
        }
    }
    #endregion
}