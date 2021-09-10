using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public bool readyToStart; //TEMP
    public Action LeftPartyWon;
    public Action RightPartyWon;
    [SerializeField] GameObject characterPrefab;
    [SerializeField] Transform enemiesParent;
    [SerializeField] SlotsCreator slotsCreator;
    [SerializeField] List<int> leftParty;
    [SerializeField] List<int> rightParty;
    [SerializeField] List<BattleCharacterHolder> holders;
    [SerializeField]
    BattleCharacterController[] characters = new BattleCharacterController[18]; //first 6 are player
    [SerializeField]
    BoxCollider2D[] characterTiles = new BoxCollider2D[18]; //second 6 are enemies (same as characters)
    BattleCharacterSO[] characterSOs;
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

        //Generating enemies
        characterSOs = Resources.LoadAll<BattleCharacterSO>("Scriptable Objects/Characters");
    }
    private void Start()
    {
        GenerateEnemies();
    }

    #region Methods
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
            UnlinkCharacterActions(character);
            if (CharacterIsOnLeft(characterIndex))
            {
                leftParty.Remove(characterIndex);
            }
            else
            {
                rightParty.Remove(characterIndex);
            }
            characters[characterIndex] = null;
        }

        //add character to array
        int slotIndex = Array.IndexOf(characterTiles, slotCollider);
        characters[slotIndex] = character;
        LinkCharacterActions(character);
        if (CharacterIsOnLeft(slotIndex))
        {
            leftParty.Add(slotIndex);
        }
        else
        {
            rightParty.Add(slotIndex);
        }
    }
    void OnCharacterSelectTarget(BattleCharacterController attacker)
    {
        attacker.target = GetAttackReceiver(attacker);
    }
    void OnCharacterDeath(BattleCharacterController character)
    {
        int deadCharaIndex = Array.IndexOf(characters, character);
        characters[deadCharaIndex] = null;
        if (CharacterIsOnLeft(deadCharaIndex))
        {
            leftParty.Remove(deadCharaIndex);
            if (leftParty.Count <= 0)
            {
                RightPartyWon?.Invoke();
            }
        }
        else
        {
            rightParty.Remove(deadCharaIndex);
            if (rightParty.Count <= 0)
            {
                LeftPartyWon?.Invoke();
            }
        }
    }
    BattleCharacterController GetAttackReceiver(BattleCharacterController attacker)
    {
        if (!readyToStart) { return null; } //TEMP needed to set enemy characters

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
        bool characterIsOnRight = CharacterIsOnLeft(attackerIndex);

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

        if(characterIsOnRight == CharacterIsOnLeft(receiverIndex)) { return null; }
        return characters[receiverIndex];
    }
    BattleCharacterController GetBothReciever(int attackerIndex)
    {
        int receiverIndex = 0;
        int weakestHealth = int.MaxValue;

        if (CharacterIsOnLeft(attackerIndex))
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

        if (CharacterIsOnLeft(attackerIndex) == CharacterIsOnLeft(receiverIndex)) { return null; }
        return characters[receiverIndex];
    }
    BattleCharacterController GetRangedReciever(int attackerIndex)
    {
        int receiverIndex = 0;
        int rowNumber = 1;
        bool characterIsOnRight = CharacterIsOnLeft(attackerIndex);

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

        if(characterIsOnRight == CharacterIsOnLeft(receiverIndex)) { return null; }
        return characters[receiverIndex];
    }
    void GenerateEnemies()
    {
        for(short i = 0; i < 5; i++)
        {
            GenerateRandomEnemy();
        }
    }
    void GenerateRandomEnemy()
    {
        int soIndex = UnityEngine.Random.Range(0, characterSOs.Length);
        int characterIndex = UnityEngine.Random.Range(0, midArray);

        while (characters[characterIndex])
        {
            characterIndex = UnityEngine.Random.Range(0, midArray);
        }

        BattleCharacterController character = Instantiate(characterPrefab, enemiesParent).GetComponent<BattleCharacterController>();
        character.transform.position = characterTiles[characterIndex].transform.position;

        leftParty.Add(characterIndex);
        character.SetData(characterSOs[soIndex]);
        LinkCharacterActions(character);

        characters[characterIndex] = character;
    }
    bool CharacterIsOnLeft(int characterIndex)
    {
        return characterIndex < midArray;
    }
    void UnlinkCharacterActions(BattleCharacterController character)
    {
        character.SelectTarget -= OnCharacterSelectTarget;
        character.Die -= OnCharacterDeath;
    }
    void LinkCharacterActions(BattleCharacterController character)
    {
        character.SelectTarget += OnCharacterSelectTarget;
        character.Die += OnCharacterDeath;
    }
    #endregion
}