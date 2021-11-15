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
    public override void ResetParty()
    {
        readyToBattle = false;

        //Remove Old party
        while (characters.Count > 0)
        {
            RemoveCharacter(characters[0]);
        }

        //Get New one
        GetCharacters();
        foreach (BattleCharacterHolder holder in holders)
        {
            holder.enabled = true;
            holder.ResetPosition();
            holder.gameObject.SetActive(true);
        }
    }
    internal override void SetAdditionalThings()
    {
        foreach (BattleCharacterHolder holder in holders)
        {
            holder.enabled = false;
        }
    }
    void GetCharacters()
    {
        Player player = Player.Get();

        for (int i = 0; i < player.lineup.Length; i++)
        {
            GameObject character = holders[i].gameObject;
            BattleCharacterController characterController = character.GetComponent<BattleCharacterController>();
            //characters.Add(characterController);
            characterController.SetData(player.lineup[i]);
            if (characterController.publicData.level >= 1)
            {
                characterController.InitCharacter();
            }
            else
            {
                characterController.InitCharacterFromZero();
            }
        }
        //NewCharactersAdded?.Invoke();
    }
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
        slotCollider.transform.tag = "SlotTaken";
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