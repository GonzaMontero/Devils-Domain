using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] SlotsCreator slotsCreator;
    [SerializeField] List<BattleCharacterHolder> holders;
    [SerializeField]
    BattleCharacterController[] characters = new BattleCharacterController[18]; //first 6 are player
    [SerializeField]
    BoxCollider2D[] characterTiles = new BoxCollider2D[18]; //second 6 are enemies (same as characters)
    int midArray;

    /*
     * slots are organized in proximity, so matrix would be:
     * 6 3 0 - 9  12 15
     * 7 4 1 - 10 13 16
     * 8 5 2 - 11 14 17
     * OR
     * 3 0 - 6 9
     * 4 1 - 7 10
     * 5 2 - 8 11
     */

    private void Awake()
    {
        midArray = characters.Length / 2;

        //Link Actions
        slotsCreator.SlotGenerated += OnSlotCreated;
        foreach (BattleCharacterHolder holder in holders)
        {
            holder.CharacterPositioned += OnCharacterPositioned;
        }
    }

    #region Methods
    void OnCharacterSelectTarget(BattleCharacterController attacker)
    {
        attacker.target = GetAttackReceiver(attacker);
    }
    void OnSlotCreated(BoxCollider2D collider)
    {
        int slotIndex = 0;
        while (slotIndex < characterTiles.Length && characterTiles[slotIndex])
        {
            slotIndex++;
        }
        if (slotIndex >= characterTiles.Length) { return; }
        characterTiles[slotIndex] = collider;
    }
    void OnCharacterPositioned(BoxCollider2D slotCollider, BattleCharacterController character)
    {
        //delete character from array if existant
        int characterIndex = Array.IndexOf(characters, character);
        if (characterIndex >= 0) 
        {
            characters[characterIndex].SelectTarget -= OnCharacterSelectTarget;
            characters[characterIndex] = null;
        }

        //add character to array
        int slotIndex = Array.IndexOf(characterTiles, slotCollider);
        characters[slotIndex] = character;
        character.SelectTarget += OnCharacterSelectTarget;
    }
    BattleCharacterController GetAttackReceiver(BattleCharacterController attacker)
    {
        int attackerIndex = Array.IndexOf(characters, attacker);
        BattleCharacterController reciever = null;

        //check if distance is correct and attack
        switch (attacker.GetAttackType())
        {
            case AttackType.melee:
                reciever = GetMeleeReciever(attackerIndex);
                break;
            case AttackType.both:
                reciever = GetBothReciever(attackerIndex);
                break;
            case AttackType.ranged:
                reciever = GetRangedReciever(attackerIndex);
                break;
            default:
                break;
        }

        return reciever;
    }
    BattleCharacterController GetMeleeReciever(int attackerIndex)
    {
        int receiverIndex = 0; //index of character who gets attacked
        int rowNumber = 1; //how many rows has the character searched for a target?
        bool characterIsOnRight = CharacterIsOnRight(attackerIndex);

        switch (attackerIndex % 3)
        {
            case 0:
            case 1:
                receiverIndex = characterIsOnRight ? midArray : 0; //if attacker is ally, set target to 6
                while (!characters[receiverIndex] || !characters[receiverIndex].IsAlive())
                {
                    receiverIndex++;
                    if (characterIsOnRight && receiverIndex >= characters.Length)
                    {
                        return null;
                    }
                    else if (!characterIsOnRight && receiverIndex > midArray)
                    {
                        return null;
                    }
                }
                break;
            case 2:
                receiverIndex = characterIsOnRight ? midArray + 2 : 2; //if attacker is on right, set target to left down
                while (!characters[receiverIndex] || !characters[receiverIndex].IsAlive())
                {
                    receiverIndex--;
                    if (characterIsOnRight && receiverIndex <= midArray)
                    {
                        receiverIndex += 3 * rowNumber;
                        rowNumber++;
                        if (receiverIndex > characters.Length)
                        {
                            return null;
                        }
                    }
                    else if (!characterIsOnRight && receiverIndex < 0)
                    {
                        receiverIndex += 3 * rowNumber;
                        rowNumber++;
                        if (receiverIndex > midArray)
                        {
                            return null;
                        }
                    }
                }
                break;
            default:
                break;
        }

        if(characterIsOnRight == CharacterIsOnRight(receiverIndex)) { return null; }
        return characters[receiverIndex];
    }
    BattleCharacterController GetBothReciever(int attackerIndex)
    {
        int receiverIndex = 0;
        int weakestHealth = int.MaxValue;

        if (CharacterIsOnRight(attackerIndex))
        {
            for (int i = midArray; i < characters.Length; i++)
            {
                if (characters[i] && characters[i].GetHealth() < weakestHealth)
                {
                    weakestHealth = characters[i].GetHealth();
                    receiverIndex = i;
                }
            }
        }
        else //attacker is enemy character
        {
            for (int i = midArray - 1; i >= 0; i--)
            {
                if (characters[i] && characters[i].GetHealth() < weakestHealth)
                {
                    weakestHealth = characters[i].GetHealth();
                    receiverIndex = i;
                }
            }
        }

        if (CharacterIsOnRight(attackerIndex) == CharacterIsOnRight(receiverIndex)) { return null; }
        return characters[receiverIndex];
    }
    BattleCharacterController GetRangedReciever(int attackerIndex)
    {
        int receiverIndex = 0;
        int rowNumber = 1;
        bool characterIsOnRight = CharacterIsOnRight(attackerIndex);

        switch (attackerIndex % 3)
        {
            case 0:
            case 1: //if attacker is on right, set target to 9
                receiverIndex = characterIsOnRight ? characters.Length - 3 : midArray - 3; 
                while (!characters[receiverIndex] || !characters[receiverIndex].IsAlive())
                {
                    receiverIndex++;
                    if (characterIsOnRight && receiverIndex >= characters.Length)
                    {
                        receiverIndex -= 3 * rowNumber;
                        rowNumber++;
                        if (receiverIndex <= midArray)
                        {
                            return null;
                        }
                    }
                    else if (!characterIsOnRight && receiverIndex > midArray)
                    {
                        receiverIndex -= 3 * rowNumber;
                        rowNumber++;
                        if (receiverIndex > midArray)
                        {
                            return null;
                        }
                    }
                }
                break;
            case 2: //if attacker is ally, set target to 11
                receiverIndex = characterIsOnRight ? characters.Length - 1 : midArray - 1; 
                while (!characters[receiverIndex] || !characters[receiverIndex].IsAlive())
                {
                    receiverIndex--;
                    if (characterIsOnRight && receiverIndex <= midArray)
                    {
                        return null;
                    }
                    else if (!characterIsOnRight && receiverIndex < 0)
                    {
                            return null;
                    }
                }
                break;
            default:
                break;
        }

        if(characterIsOnRight == CharacterIsOnRight(receiverIndex)) { return null; }
        return characters[receiverIndex];
    }
    bool CharacterIsOnRight(int characterIndex)
    {
        return characterIndex < characters.Length / 2;
    }
    #endregion
}