﻿using System;
using UnityEngine;

public class BattleCharacterHolder : MonoBehaviour
{
    public Action<BoxCollider2D, BattleCharacterController> CharacterPositioned;
    public Action<BattleCharacterController> CharacterRemoved;
    [SerializeField] Transform top;
    [SerializeField] Transform bottom;
    Vector3 originalPosition;   
    Vector3 newPosition;
    LayerMask slotsMask;
    RaycastHit2D slotHitted;
    BoxCollider2D boxCollider;
    SpriteRenderer lastSlotSprite;

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
        //transform.localPosition = (Vector2)transform.localPosition; //set local Z to 0
        Debug.Log("Moved Pos To Target Pos!");
    }
    #endregion

    void GetSlot()
    {
        Vector2 pos = transform.position;// + boxCollider.size.y / 2 * Vector2.down;

        slotHitted = Physics2D.Raycast(pos, Vector2.down, boxCollider.size.y / 4, slotsMask);
        if (slotHitted && !slotHitted.transform.CompareTag("SlotTaken"))
        {
            //Debug.Log("Collided");
            UpdateSlotColor(slotHitted);
        }
        else
        {
            //Debug.Log("Not Collided");
            
            if (lastSlotSprite)
            {
                lastSlotSprite.color = Color.white; //character is not touching any slot, so color marking is not needed
            }
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