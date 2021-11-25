using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UIBattleManager : MonoBehaviour
{
	[SerializeField] BattleManager battleManager;
	[SerializeField] GameObject resetButton;
	[SerializeField] TextMeshProUGUI resetButtonText;
	[SerializeField] GameObject victoryImage;
	[SerializeField] GameObject defeatImage;
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
        //Disable Game Over UI
        resetButton.SetActive(false);
        defeatImage.SetActive(false);
        victoryImage.SetActive(false);
        
        //Reset Battle
        battleManager.ResetLevel();

        //Enable Start UI
        playButton.enabled = true;
        playButtonImage.color = Color.white;
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
        resetButtonText.text = "Try Again!";
        resetButton.SetActive(true);
        defeatImage.SetActive(true);
    }
    void OnVictory()
    {
        resetButtonText.text = "Next Level";
        resetButton.SetActive(true);
        victoryImage.SetActive(true);
    }
}