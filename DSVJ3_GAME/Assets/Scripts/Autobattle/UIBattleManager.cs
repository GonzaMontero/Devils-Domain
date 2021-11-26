using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UIBattleManager : MonoBehaviour
{
	[SerializeField] BattleManager battleManager;
	[SerializeField] GameObject victoryScreen;
	[SerializeField] GameObject defeatScreen;
	[SerializeField] GameObject exitButton;
    [SerializeField] Image playButtonImage;
	[SerializeField] Button playButton;
    [SerializeField] float noCharactersWarningDuration;

    //Unity Events
    private void Start()
    {
        battleManager.PlayerPartyWon += OnVictory;
        battleManager.EnemyPartyWon += OnDefeat;

        GameManager gameManager = GameManager.Get();
    }

    //Methods
    public void StartGame()
    {
        if(!battleManager.ReadyForBattle())
        {
            playButtonImage.color = Color.red;
            StopAllCoroutines();
            StartCoroutine(ResetButtonColor(playButtonImage));
            return;
        }

        playButton.enabled = false;
		playButtonImage.color = Color.gray;
        battleManager.StartGame();
    }
    public void ResetLevel()
    {
        //Reset Battle
        battleManager.ResetLevel();

        RemoveGameOverUI();
    }
    public void NextLevel()
    {
        //Reset Battle
        battleManager.NextLevel();

        RemoveGameOverUI();
    }
    void RemoveGameOverUI()
    {
        //Disable Game Over UI
        victoryScreen.SetActive(false);
        defeatScreen.SetActive(false);

        //Enable Start UI
        playButton.enabled = true;
        playButtonImage.color = Color.white;
	    playButton.gameObject.SetActive(true);
    }
    IEnumerator ResetButtonColor(Image buttonImage)
    {
        float timer = 0;
        while (timer < noCharactersWarningDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        
        buttonImage.color = Color.white;
        yield break;
    }

    //Event Receivers
    void OnDefeat()
    {
	    playButton.gameObject.SetActive(false);
        exitButton.SetActive(false);
        victoryScreen.SetActive(false);
        defeatScreen.SetActive(true);
    }
    void OnVictory()
    {
	    playButton.gameObject.SetActive(false);
        exitButton.SetActive(false);
        victoryScreen.SetActive(true);
        defeatScreen.SetActive(false);
    }
}