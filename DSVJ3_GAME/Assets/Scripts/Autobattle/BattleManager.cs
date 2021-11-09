using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Action EnemyPartyWon;
    public Action PlayerPartyWon;
    [SerializeField] GameObject characterPrefab;
    [SerializeField] Transform enemiesParent;
    //[SerializeField] SlotsCreator slotsCreator;
    [SerializeField] List<BattleCharacterController> enemies;
    [SerializeField] List<BattleCharacterController> allies;
    [SerializeField] List<BattleCharacterHolder> holders;
    [SerializeField] BoxCollider2D[] characterTiles = new BoxCollider2D[18];
    [SerializeField] int battleXP;
    [SerializeField] int battleGold;
    BattleCharacterSO[] characterSOs;
    Dictionary<int, BattleCharacterController> enemyInTile;
    Dictionary<int, BattleCharacterController> allyInTile;
    Dictionary<BattleCharacterController, int> tileOfEnemy;
    Dictionary<BattleCharacterController, int> tileOfAlly;
    BattlePlayer player;
    bool readyToStart = false;

    private void Start()
    {
        readyToStart = false;

        enemyInTile = new Dictionary<int, BattleCharacterController>();
        allyInTile = new Dictionary<int, BattleCharacterController>();
        tileOfEnemy = new Dictionary<BattleCharacterController, int>();
        tileOfAlly = new Dictionary<BattleCharacterController, int>();

        //Link Actions
        foreach (BattleCharacterHolder holder in holders)
        {
            holder.CharacterPositioned += OnCharacterPositioned;
            holder.CharacterRemoved += OnCharacterRemoved;
        }

        //Generating enemies
        characterSOs = Resources.LoadAll<BattleCharacterSO>("Scriptable Objects/Characters");

        //slotsCreator = FindObjectOfType<SlotsCreator>();
        //AddSlots();
        GenerateEnemies();
        CalculateRewards();
    }

    public void StartGame()
    {
        player = BattlePlayer.Get();
        //if (player.characters.Count > 0)
        //{
        //    int i = 0;
        //    foreach (var ally in player.characters)
        //    {
        //        AddCharacter(true, ally, i); //TEMP, i IS PLACEHOLDER
        //        i++;
        //    }
        //}

        readyToStart = true;
        foreach (BattleCharacterHolder holder in holders)
        {
            Destroy(holder);
        }
    }
    /*void AddSlots()
    {
        slotsCreator.slotList.CopyTo(characterTiles);
    }*/
    void OnCharacterPositioned(BoxCollider2D slotCollider, BattleCharacterController character)
    {
        int tileIndex = Array.IndexOf(characterTiles, slotCollider);
        bool characterIsAlly = tileIndex >= characterTiles.Length / 2;

        //delete character from list if existant
        if (allies.Contains(character) || enemies.Contains(character))
        {
            int oldSlotIndex;
            if (characterIsAlly)
            {
                tileOfAlly.TryGetValue(character, out oldSlotIndex);
            }
            else
            {
                tileOfEnemy.TryGetValue(character, out oldSlotIndex);
            }
            characterTiles[oldSlotIndex].tag = "Slot";
            RemoveCharacter(characterIsAlly, character);
        }

        //add character to list
        AddCharacter(characterIsAlly, character, tileIndex);
        slotCollider.transform.tag = "SlotTaken";
    }
    void OnCharacterRemoved(BattleCharacterController character)
    {
        //delete character from list it is existant
        int oldSlotIndex = -1;

        if (allies.Contains(character))
        {
            tileOfAlly.TryGetValue(character, out oldSlotIndex);

            RemoveCharacter(true, character);
        }
        else if (enemies.Contains(character))
        {
            tileOfEnemy.TryGetValue(character, out oldSlotIndex);
            RemoveCharacter(false, character);
        }

        if (oldSlotIndex != -1)
        {
            characterTiles[oldSlotIndex].tag = "Slot";
        }
    }
    void OnCharacterSelectTarget(BattleCharacterController attacker)
    {
        attacker.target = GetAttackReceiver(attacker);
    }
    void OnCharacterDeath(BattleCharacterController character)
    {
        if (CharacterIsAlly(character))
        {
            RemoveCharacter(true, character);
            if (allies.Count <= 0)
            {
                EnemyWon();
            }
        }
        else
        {
            RemoveCharacter(false, character);
            if (enemies.Count <= 0)
            {
                PlayerWon();
            }
        }
    }
    BattleCharacterController GetAttackReceiver(BattleCharacterController attacker)
    {
        if (!readyToStart || allies.Count < 1 || enemies.Count < 1) { return null; } //TEMP needed to set enemy characters
        BattleCharacterController reciever = null;

        //check if distance is correct and attack
        switch (attacker.GetAttackType())
        {
            case AttackType.melee:
            case AttackType.ranged:
                reciever = GetReciever(attacker);
                break;
            case AttackType.assasin:
                reciever = GetWeakestReciever(attacker);
                break;
            default:
                break;
        }

        return reciever;
    }
    BattleCharacterController GetReciever(BattleCharacterController attacker)
    {
        if (CharacterIsAlly(attacker) && enemies.Count < 1) { return null; }
        else if (allies.Count < 1) { return null; }


        return CharacterIsAlly(attacker) ? enemies[0] : allies[0];
    }
    BattleCharacterController GetWeakestReciever(BattleCharacterController attacker)
    {
        BattleCharacterController receiver = null;
        int weakestHealth = int.MaxValue;

        if (CharacterIsAlly(attacker))
        {
            foreach (BattleCharacterController enemy in enemies)
            {
                if (enemy.publicData.health < weakestHealth)
                {
                    weakestHealth = enemy.publicData.health;
                    receiver = enemy;
                }
            }
        }
        else //attacker is enemy character
        {
            foreach (BattleCharacterController ally in allies)
            {
                if (ally.publicData.health < weakestHealth)
                {
                    weakestHealth = ally.publicData.health;
                    receiver = ally;
                }
            }
        }

        return receiver;
    }
    void GenerateEnemies()
    {
        for (short i = 0; i < 5; i++)
        {
            GenerateRandomEnemy();
        }
    }
    void GenerateRandomEnemy()
    {
        int soIndex = UnityEngine.Random.Range(0, characterSOs.Length);
        int tileIndex = UnityEngine.Random.Range(0, characterTiles.Length / 2);

        while (enemyInTile.ContainsKey(tileIndex))
        {
            tileIndex = UnityEngine.Random.Range(0, characterTiles.Length / 2);
        }

        BattleCharacterController character = Instantiate(characterPrefab, enemiesParent).GetComponent<BattleCharacterController>();
        character.transform.position = characterTiles[tileIndex].transform.position;
        character.GetComponent<SpriteRenderer>().flipX = true;
        character.GetComponent<SpriteRenderer>().sortingOrder = -(int)character.transform.position.z;

        character.SetData(characterSOs[soIndex]);
        AddCharacter(false, character, tileIndex);
    }
    bool CharacterIsAlly(BattleCharacterController character)
    {
        return allies.Contains(character);
    }
    void AddCharacter(bool isAlly, BattleCharacterController character, int tileIndex)
    {
        LinkCharacterActions(character);
        if (isAlly)
        {
            allies.Add(character);
            SetAllyInTile(tileIndex, character);
        }
        else
        {
            enemies.Add(character);
            SetEnemyInTile(tileIndex, character);
        }
    }
    void RemoveCharacter(bool isAlly, BattleCharacterController character = null, int tileIndex = -1)
    {
        UnlinkCharacterActions(character);
        if (isAlly)
        {
            allies.Remove(character);
            RemoveAllyInTile(tileIndex, character);
        }
        else
        {
            enemies.Remove(character);
            RemoveEnemyInTile(tileIndex, character);
        }
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
    void SetEnemyInTile(int tileIndex, BattleCharacterController character)
    {
        enemyInTile.Add(tileIndex, character);
        tileOfEnemy.Add(character, tileIndex);
    }
    void SetAllyInTile(int tileIndex, BattleCharacterController character)
    {
        allyInTile.Add(tileIndex, character);
        tileOfAlly.Add(character, tileIndex);
    }
    void RemoveEnemyInTile(int tileIndex = -1, BattleCharacterController character = null)
    {
        //Get missing values from respective Dictionary
        if (tileIndex == -1)
        {
            if (!tileOfEnemy.TryGetValue(character, out tileIndex))
            {
                return;
            }
        }
        else
        {
            if (!enemyInTile.TryGetValue(tileIndex, out character))
            {
                return;
            }
        }

        enemyInTile.Remove(tileIndex);
        tileOfEnemy.Remove(character);
    }
    void RemoveAllyInTile(int tileIndex = -1, BattleCharacterController character = null)
    {
        //Get missing values from respective Dictionary
        if (tileIndex == -1)
        {
            if (!tileOfAlly.TryGetValue(character, out tileIndex))
            {
                return;
            }
        }
        else
        {
            if (!allyInTile.TryGetValue(tileIndex, out character))
            {
                return;
            }
        }

        allyInTile.Remove(tileIndex);
        tileOfAlly.Remove(character);
    }
    void PlayerWon()
    {
        GiveRewards();
        SaveAllies(); //save player characters to play in next stage
        PlayerPartyWon?.Invoke();
    }
    void EnemyWon()
    {
        Destroy(player.gameObject); //reset player characters so they CAN appear next stage
        EnemyPartyWon?.Invoke();
    }
    void CalculateRewards()
    {
        foreach (var enemy in enemies)
        {
            battleXP += enemy.publicData.so.baseXpToLevelUp;
        }
        battleGold = battleXP / 2;
    }
    void GiveRewards()
    {
        foreach (var ally in allies)
        {
            ally.ReceiveXP(battleXP);
        }
        player.AddGold(battleGold);
    }
    void SaveAllies()
    {
        player.characters.Clear();
        for (int i = 0; i < characterTiles.Length; i++)
        {
            BattleCharacterController ally;
            if (allyInTile.TryGetValue(i, out ally))
            {
                player.characters.Add(ally);
            }
        }
    }
}