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
        AudioManager.SetAutobattleMusic();
    }
    public void NextLevel()
    {
        player.level++;
        enemyParty.ResetParty();
        playerParty.ResetParty();
        levelM.ResetLevel();
        AudioManager.SetAutobattleMusic();
    }
    public bool ReadyForBattle()
    {
        return playerParty.characters.Count > 0 && enemyParty.characters.Count > 0;
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
        
        //Init lists
        BattleCharacterData[] charactersToSave = new BattleCharacterData[6];
        List<BattleCharacterController> characters = playerParty.GetParty();
        
        //get datas
        for (int i = 0; i < charactersToSave.Length; i++)
        {
            if (characters.Count <= i) break; //if i is bigger than list, break loop
            if (!characters[i]) continue; //if there is no character, skip iteration

            charactersToSave[i] = characters[i].publicData; //save character data
        }

        player.lineup = charactersToSave;
    }

    //Event Receivers
    void OnPlayerWon()
    {
        GiveRewards();
        SaveAllies(); //save player characters to play in next stage
        AudioManager.SetVictoryMusic();
        PlayerPartyWon?.Invoke();
    }
    void OnEnemyWon()
    {
        EnemyPartyWon?.Invoke();
    }
}