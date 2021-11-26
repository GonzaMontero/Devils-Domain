using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParty : PartyManager
{
    [SerializeField] List<BattleCharacterHolder> holders;

    //Unity Events
    private void Start()
    {
        GetCharacters();

        foreach (BattleCharacterHolder holder in holders)
        {
            holder.CharacterPositioned += OnCharacterPositioned;
            holder.CharacterRemoved += OnCharacterRemoved;
        }
    }

    //Methods
    public override List<BattleCharacterController> GetParty()
    {
        List<BattleCharacterController> charactersToSave = new List<BattleCharacterController>();
        Player player = Player.Get();
        for (int i = 0; i < player.lineup.Length; i++)
        {
            if (holders.Count <= i) break; //if i is bigger than list, break loop
            if (!holders[i]) continue; //if there is no holder, skip iteration
            
            GameObject character = holders[i].gameObject;
            BattleCharacterController characterController = character.GetComponent<BattleCharacterController>();
            if (characterController.publicData != null)
            {
                charactersToSave.Add(characterController);
            }
        }
        return charactersToSave;
    }
    public override void ResetParty()
    {
        readyToBattle = false;

        //Remove Old party
        while (characters.Count > 0)
        {
            RemoveCharacter(characters[0]);
        }

        //Get New one
        foreach (BattleCharacterHolder holder in holders)
        {
            holder.enabled = true;
            holder.ResetPosition();
            holder.gameObject.SetActive(true);
        }
        GetCharacters();
    }
    internal override void PostReadyPartyForBattle()
    {
        for (int i = 0; i < holders.Count; i++)
        {
            if (holders[i]) //if there is holder
            {                
                holders[i].enabled = false; //disable holder
                continue; //skip iteration
            }

            //Remove null holders
            do
            {
                holders.RemoveAt(i);
                if (i >= holders.Count) break; //if i is equal or bigger than list size, end while
            } while (!holders[i]);
        }
    }
    void GetCharacters()
    {
        Player player = Player.Get();

        for (int i = 0; i < player.lineup.Length; i++)
        {
            if (holders.Count <= i) break; //if i is bigger than list, break loop
            if (!holders[i]) continue; //if there is no holder, skip iteration
            if (!player.lineup[i].so) //if there is no data, destroy character and skip iteration
            {
                Destroy(holders[i].gameObject);
                continue; 
            }

            GameObject character = holders[i].gameObject;
            BattleCharacterController characterController = character.GetComponent<BattleCharacterController>();
            characterController.SetData(player.lineup[i]);
            characterController.InitCharacter();
        }
    }

    //Event Receivers
    void OnCharacterPositioned(BoxCollider2D slotCollider, BattleCharacterController character)
    {
        int tileIndex = Array.IndexOf(characterTiles, slotCollider);

        //delete character from list if existant
        if (characters.Contains(character))
        {
            int oldSlotIndex;
            tileOfCharacter.TryGetValue(character, out oldSlotIndex);
            characterTiles[oldSlotIndex].tag = "Slot";
            RemoveCharacter(character);
        }

        //add character to list
        AddCharacter(character, tileIndex);
        slotCollider.tag = "SlotTaken";

        //update character sorting order
        int slotSortNumber = slotCollider.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        character.GetComponent<SpriteRenderer>().sortingOrder = slotSortNumber + 1;
    }
    void OnCharacterRemoved(BattleCharacterController character)
    {
        //delete character from list it is existant
        int oldSlotIndex = -1;

        if (characters.Contains(character))
        {
            tileOfCharacter.TryGetValue(character, out oldSlotIndex);

            RemoveCharacter(character);
        }

        if (oldSlotIndex != -1)
        {
            characterTiles[oldSlotIndex].tag = "Slot";
        }
    }
}