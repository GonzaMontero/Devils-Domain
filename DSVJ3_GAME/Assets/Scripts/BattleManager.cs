using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    BattleCharacterController[] characters = new BattleCharacterController[12]; //first 6 are player
    //Tile[] characterTiles = new Tile[12]; //second 6 are enemies (same as characters)
    
    /*
     * slots are organized in proximity, so matrix would be:
     * 3 0 - 6 9
     * 4 1 - 7 10
     * 5 2 - 8 11
     */

    #region Unity Events
    private void Start()
    {
        //Get Characters


        //Link
        foreach (BattleCharacterController character in characters)
        {
            character.SelectTarget += OnCharacterSelectTarget;
        }
    }
    #endregion

    #region Methods
    void OnCharacterSelectTarget(BattleCharacterController attacker)
    {
        attacker.target = GetAttackReceiver(attacker);
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
        int receiverIndex = 0;
        bool characterIsAlly = CharacterIsAlly(attackerIndex);

        switch (attackerIndex % 3)
        {
            case 0:
                receiverIndex = characterIsAlly ? 6 : 0; //if attacker is ally, set target to 6
                while (!characters[receiverIndex].IsAlive())
                {
                    receiverIndex++;
                }
                break;
            case 1:
                receiverIndex = characterIsAlly ? 7 : 1; //if attacker is ally, set target to 7
                while (!characters[receiverIndex].IsAlive())
                {
                    receiverIndex++;
                    if (receiverIndex >= characters.Length)
                    {
                        receiverIndex -= 5;
                    }
                }
                break;
            case 2:
                receiverIndex = characterIsAlly ? 8 : 2; //if attacker is ally, set target to 8
                while (!characters[receiverIndex].IsAlive())
                {
                    receiverIndex--;
                }
                break;
            default:
                break;
        }

        return characters[receiverIndex];
    }
    BattleCharacterController GetBothReciever(int attackerIndex)
    {
        int receiverIndex = 0;
        int weakestHealth = int.MaxValue;

        if (CharacterIsAlly(attackerIndex))
        {
            for (int i = 6; i < characters.Length; i++)
            {
                if (characters[i].GetHealth() < weakestHealth)
                {
                    weakestHealth = characters[i].GetHealth();
                    receiverIndex = i;
                }
            }
        }
        else //attacker is enemy character
        {
            for (int i = 5; i >= 0; i--)
            {
                if (characters[i].GetHealth() < weakestHealth)
                {
                    weakestHealth = characters[i].GetHealth();
                    receiverIndex = i;
                }
            }
        }

        return characters[receiverIndex];
    }
    BattleCharacterController GetRangedReciever(int attackerIndex)
    {
        int receiverIndex = 0;
        bool characterIsAlly = CharacterIsAlly(attackerIndex);

        switch (attackerIndex % 3)
        {
            case 0:
                receiverIndex = characterIsAlly ? 9 : 3; //if attacker is ally, set target to 6
                while (!characters[receiverIndex].IsAlive())
                {
                    receiverIndex++;
                }
                break;
            case 1:
                receiverIndex = characterIsAlly ? 10 : 4; //if attacker is ally, set target to 7
                while (!characters[receiverIndex].IsAlive())
                {
                    receiverIndex++;
                    if (receiverIndex >= characters.Length)
                    {
                        receiverIndex -= 5;
                    }
                }
                break;
            case 2:
                receiverIndex = characterIsAlly ? 11 : 5; //if attacker is ally, set target to 8
                while (!characters[receiverIndex].IsAlive())
                {
                    receiverIndex--;
                }
                break;
            default:
                break;
        }

        return characters[receiverIndex];
    }
    bool CharacterIsAlly(int characterIndex)
    {
        return characterIndex < 6;
    }
    #endregion
}