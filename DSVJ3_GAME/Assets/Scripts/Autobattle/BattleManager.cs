using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Action EnemyPartyWon;
    public Action PlayerPartyWon;
    public Dictionary<int, BattleCharacterController> enemyInTile;
    public Dictionary<int, BattleCharacterController> allyInTile;
    [SerializeField] GameObject characterPrefab;
    [SerializeField] Transform enemiesParent;
    [SerializeField] SlotsCreator slotsCreator;
    [SerializeField] List<BattleCharacterController> enemies;
    [SerializeField] List<BattleCharacterController> allies;
    [SerializeField] List<BattleCharacterHolder> holders;
    [SerializeField] BoxCollider2D[] characterTiles = new BoxCollider2D[18];
    BattleCharacterSO[] characterSOs;
    bool readyToStart;

    private void Awake()
    {
        enemyInTile = new Dictionary<int, BattleCharacterController>();
        allyInTile = new Dictionary<int, BattleCharacterController>();

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
    public void StartGame()
    {
        readyToStart = true;
        foreach (BattleCharacterHolder holder in holders)
        {
            Destroy(holder);
        }
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
        if (allies.Contains(character) || enemies.Contains(character))
        {
            UnlinkCharacterActions(character);
            if (CharacterIsAlly(character))
            {
                enemies.Remove(character);
            }
            else
            {
                allies.Remove(character);
            }
        }

        //add character to list
        LinkCharacterActions(character);
        if (CharacterIsAlly(character))
        {
            enemies.Add(character);
        }
        else
        {
            allies.Add(character);
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
            enemies.Remove(character);
            if (enemies.Count <= 0)
            {
                PlayerPartyWon?.Invoke();
            }
        }
        else
        {
            allies.Remove(character);
            if (allies.Count <= 0)
            {
                EnemyPartyWon?.Invoke();
            }
        }
    }
    BattleCharacterController GetAttackReceiver(BattleCharacterController attacker)
    {
        if (!readyToStart) { return null; } //TEMP needed to set enemy characters
        BattleCharacterController reciever = null;

        //check if distance is correct and attack
        switch (attacker.GetAttackType())
        {
            case AttackType.melee:
            case AttackType.ranged:
                reciever = GetReciever(attacker);
                break;
            case AttackType.both:
                reciever = GetWeakestReciever(attacker);
                break;
            default:
                break;
        }

        return reciever;
    }
    BattleCharacterController GetReciever(BattleCharacterController attacker)
    {
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
                if (enemy.GetHealth() < weakestHealth)
                {
                    weakestHealth = enemy.GetHealth();
                    receiver = enemy;
                }
            }
        }
        else //attacker is enemy character
        {
            foreach (BattleCharacterController ally in allies)
            {
                if (ally.GetHealth() < weakestHealth)
                {
                    weakestHealth = ally.GetHealth();
                    receiver = ally;
                }
            }
        }

        return receiver;
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
        int tileIndex = UnityEngine.Random.Range(0, characterTiles.Length / 2);

        while (enemyInTile.ContainsKey(tileIndex))
        {
            tileIndex = UnityEngine.Random.Range(0, characterTiles.Length / 2);
        }

        BattleCharacterController character = Instantiate(characterPrefab, enemiesParent).GetComponent<BattleCharacterController>();
        character.transform.position = characterTiles[tileIndex].transform.position;
        character.GetComponent<SpriteRenderer>().flipX = true;

        enemies.Add(character);
        character.SetData(characterSOs[soIndex]);
        LinkCharacterActions(character);
    }
    bool CharacterIsAlly(BattleCharacterController character)
    {
        return allies.Contains(character);
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