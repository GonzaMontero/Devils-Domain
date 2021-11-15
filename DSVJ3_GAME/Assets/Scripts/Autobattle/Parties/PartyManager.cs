﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour, IComparer<BattleCharacterController>
{
    public Action PartyLost;
    [SerializeField] internal BoxCollider2D[] characterTiles = new BoxCollider2D[9];
	[SerializeField] internal List<BattleCharacterController> characters;
    internal Dictionary<int, BattleCharacterController> characterInTile;
    internal Dictionary<BattleCharacterController, int> tileOfCharacter;
    internal bool readyToBattle = false;
    PartyManager enemyManager;

    //Unity Events
    internal void Awake()
    {
        characterInTile = new Dictionary<int, BattleCharacterController>();
        tileOfCharacter = new Dictionary<BattleCharacterController, int>();
    }

    //Methods
    public List<BattleCharacterController> GetParty()
    {
        return characters;
    }
    public int GetPartyXPValue()
    {
        int battleXP = 0;
        foreach (var character in characters)
        {
            battleXP += character.publicData.so.baseXpToLevelUp;
        }
        return battleXP;
    }
    public int GetPartyGoldValue()
    {
        int battleXP = 0;
        foreach (var character in characters)
        {
            battleXP += character.publicData.so.baseXpToLevelUp;
        }
        return battleXP / 2; //battle gold = battle xp/2
    }
    public void GiveXPToParty(int xpForEachCharacter)
    {
        foreach (var character in characters)
        {
            character.ReceiveXP(xpForEachCharacter);
        }
    }
    public void SetEnemyManager(PartyManager newEnemyManager)
    {
        enemyManager = newEnemyManager;
    }
    public virtual void ResetParty() { }
    public void ReadyPartyForBattle()
    {
        foreach (BattleCharacterController character in characters)
        {
            LinkCharacterActions(character);
        }
        SortCharacters();
        SetStartOfBattleStats();
        SetAdditionalThings();


        readyToBattle = true;
    }
    internal virtual void SetAdditionalThings() { }
    internal void AddCharacter(BattleCharacterController character, int tileIndex)
    {
        characters.Add(character);
        SetCharacterInTile(tileIndex, character);
    }    
    internal void RemoveCharacter(BattleCharacterController character = null, int tileIndex = -1)
    {
        UnlinkCharacterActions(character);
        characters.Remove(character);
        RemoveCharacterInTile(tileIndex, character);
    }
    void SortCharacters()
    {
        characters.Sort(Compare);

        ///////////////OLD METHOD
        //List<BattleCharacterController> sortedList = new List<BattleCharacterController>();
        //int characterCount = characters.Count;
        //int lowestIndexValue;
        //int currentValue;
        //BattleCharacterController lowestIndexCharacter;
        //do
        //{
        //    //Get character in the lowest index tile
        //    lowestIndexValue = int.MaxValue;
        //    foreach (BattleCharacterController character in characters)
        //    {
        //        tileOfCharacter.TryGetValue(character, out currentValue);
        //        if (currentValue < lowestIndexValue)
        //        {
        //            lowestIndexValue = currentValue;
        //        }
        //    }

        //    //Move Character from old list to sorted list
        //    characterInTile.TryGetValue(lowestIndexValue, out lowestIndexCharacter);
        //    characters.Remove(lowestIndexCharacter);
        //    sortedList.Add(lowestIndexCharacter);
        //} while (sortedList.Count < characterCount);
    }
    void SetStartOfBattleStats()
    {
        foreach (var character in characters)
        {
            if (character.publicData.level >= 1)
            {
                character.InitCharacter();
            }
            else
            {
                character.InitCharacterFromZero();
            }
        }
    }
    void LinkCharacterActions(BattleCharacterController character)
    {
        character.SearchForTarget += enemyManager.GetAttackReceiver;
        character.Die += OnCharacterDeath;
    }
    void UnlinkCharacterActions(BattleCharacterController character)
    {
        character.SearchForTarget -= enemyManager.GetAttackReceiver;
        character.Die -= OnCharacterDeath;
    }
    void SetCharacterInTile(int tileIndex, BattleCharacterController character)
    {
        characterInTile.Add(tileIndex, character);
        tileOfCharacter.Add(character, tileIndex);
    }
    void RemoveCharacterInTile(int tileIndex = -1, BattleCharacterController character = null)
    {
        //Get missing values from respective Dictionary
        if (tileIndex == -1)
        {
            if (!tileOfCharacter.TryGetValue(character, out tileIndex))
            {
                return;
            }
        }
        else
        {
            if (!characterInTile.TryGetValue(tileIndex, out character))
            {
                return;
            }
        }

        characterInTile.Remove(tileIndex);
        tileOfCharacter.Remove(character);
    }
    void GetAttackReceiver(BattleCharacterController attacker)
    {
        if (!readyToBattle) return;

        BattleCharacterController reciever = null;

        //check if distance is correct and attack
        switch (attacker.publicData.so.attackType)
        {
            case AttackType.melee:
            case AttackType.ranged:
                reciever = GetReciever(attacker.publicData.so.attackType);
                break;
            case AttackType.assasin:
                reciever = GetWeakestReciever();
                break;
            default:
                break;
        }

        attacker.target = reciever;
    }
    BattleCharacterController GetReciever(AttackType attackerType)
    {
        if (characters.Count < 1) return null; //if there are no characters, return nothing

        //return first character if attacker is melee, return last if ranged
        return attackerType == AttackType.melee ? characters[0] : characters[characters.Count - 1];
    }
    BattleCharacterController GetWeakestReciever()
    {
        BattleCharacterController receiver = null;
        int weakestHealth = int.MaxValue;

        //Search for the weakest reciever in list
        foreach (BattleCharacterController character in characters)
        {
            if (character.publicData.health < weakestHealth)
            {
                weakestHealth = character.publicData.health;
                receiver = character;
            }
        }

        return receiver;
    }

    //Event Receivers    
    void OnCharacterDeath(BattleCharacterController character)
    {
        RemoveCharacter(character);
        if (characters.Count <= 0)
        {
            PartyLost?.Invoke();
        }
    }

    //Interface implementations
    public int Compare(BattleCharacterController x, BattleCharacterController y)
    {
        //Declare index ints
        int xTileIndex;
        int yTileIndex;

        //Get tile indexes
        if(!tileOfCharacter.TryGetValue(x, out xTileIndex)) return -1;
        if (!tileOfCharacter.TryGetValue(y, out yTileIndex)) return -1;

        // CompareTo() method
        return xTileIndex.CompareTo(yTileIndex);
    }
}