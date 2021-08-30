using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    BattleCharacterController[] characters = new BattleCharacterController[12]; //first 6 are player
    Tile[] characterTiles = new Tile[12]; //second 6 are enemies (same as characters)
    
    /*
     * slots are organized in proximity, so matrix would be:
     * 1 0 - 6 7
     * 3 2 - 8 9
     * 4 5 - 10 11
     */

    #region Unity Events
    private void Start()
    {
        //Get Characters


        //Link
        foreach (BattleCharacterController character in characters)
        {
            character.Attack += OnCharacterAttack;
        }
    }
    #endregion

    #region Methods
    void OnCharacterAttack(BattleCharacterController attacker)
    {
        int receiverNumber = Array.IndexOf(characters, attacker); //get index of attacker
        receiverNumber += receiverNumber >= 6 ? -6 : 6; //get parallel index (0 - 6)

        //check if distance is correct and attack
        switch (attacker.GetAttackType())
        {
            case AttackType.melee:
                if (receiverNumber % 2 != 0) { return; } //only attack front liners
                characters[receiverNumber]?.ReceiveDamage(attacker.GetAttackDamage());
                break;
            case AttackType.both:
                characters[receiverNumber]?.ReceiveDamage(attacker.GetAttackDamage());
                break;
            case AttackType.ranged:
                if (receiverNumber % 2 == 0) { return; } //only attack back liners
                characters[receiverNumber]?.ReceiveDamage(attacker.GetAttackDamage());
                break;
            default:
                break;
        }
    }
    #endregion
}