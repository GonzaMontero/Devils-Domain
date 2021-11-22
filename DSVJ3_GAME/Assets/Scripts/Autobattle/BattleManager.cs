using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Action EnemyPartyWon;
    public Action PlayerPartyWon;
    [SerializeField] PartyManager playerParty;
    [SerializeField] PartyManager enemyParty;
    [SerializeField] LevelManager levelM;
    [SerializeField] int battleXP;
    [SerializeField] int battleGold;
    Player player;

    //Unity Events
    private void Start()
    {
        //Get Player
        player = Player.Get();
        
        //Set parties
        playerParty.PartyLost += OnEnemyWon;
        enemyParty.PartyLost += OnPlayerWon;
        playerParty.SetEnemyManager(enemyParty);
        enemyParty.SetEnemyManager(playerParty);
        
        //Calculate battle rewards
        CalculateRewards();
    }
    private void OnDestroy()
    {
        SaveAllies();
    }

    //Methods
    public void StartGame()
    {
        playerParty.ReadyPartyForBattle();
        enemyParty.ReadyPartyForBattle();
    }
    public void ResetLevel()
    {
        enemyParty.ResetParty();
        playerParty.ResetParty();
        levelM.ResetLevel();
    }
    void CalculateRewards()
    {
        battleXP = enemyParty.GetPartyXPValue();   
        battleGold = enemyParty.GetPartyGoldValue();   
    }
    void GiveRewards()
    {
        playerParty.GiveXPToParty(battleXP);   
        player.gold += battleGold;
    }
    void SaveAllies()
    {
        if (playerParty.characters.Count < 1) return;
        
        List<BattleCharacterData> charactersToSave = new List<BattleCharacterData>();
        foreach (var character in playerParty.GetParty())
        {
            charactersToSave.Add(character.publicData);
        }
        player.lineup = charactersToSave.ToArray();
    }

    //Event Receivers
    void OnPlayerWon()
    {
        GiveRewards();
        SaveAllies(); //save player characters to play in next stage
        player.level++;
        PlayerPartyWon?.Invoke();
    }
    void OnEnemyWon()
    {
        EnemyPartyWon?.Invoke();
    }
}