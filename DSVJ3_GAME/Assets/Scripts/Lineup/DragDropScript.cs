﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDropScript : MonoBehaviour, IPointerDownHandler
{
    private int positionOnCharacterCount;
    public Player player1;
    private GameObject characterThatWillBeSwapped;

    private void Start()
    {
        player1 = Player.Get();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (characterThatWillBeSwapped != null)
        {
            Sprite placeHolder = characterThatWillBeSwapped.GetComponent<SwapLineupSpot>().GetSprite();
            characterThatWillBeSwapped.GetComponent<SwapLineupSpot>().swapPositionOnArray(player1.characters[positionOnCharacterCount]);
            this.GetComponent<Image>().sprite = placeHolder;
        }
    }

    public void SetCharacterToSwap(GameObject characterToSwap)
    {
        characterThatWillBeSwapped = characterToSwap;
    }

    public void SetValue(int val)
    {
        positionOnCharacterCount = val;
    }

    /*Todo esto se bugea
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
    public void OnMouseUp()
    {
        GetSlot();
    }
    void GetSlot()
    {
        Vector2 pos = (Vector2)transform.position + box.size.y / 2 * Vector2.down;
        //Vector2 hitPos = pos + Vector2.up * 4;

        slotHitted = Physics2D.BoxCast(pos, Vector2.one, 0, Vector2.up, 0.1f, mask);

        if (slotHitted && !slotHitted.transform.CompareTag("Character Team Holder"))
        {
            Debug.Log("Collided");
        }
        else
        {
            Debug.Log("Not Collided");
        }
    }
    */
}