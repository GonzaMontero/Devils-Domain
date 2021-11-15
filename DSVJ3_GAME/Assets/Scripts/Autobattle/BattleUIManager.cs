using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIManager : MonoBehaviour
{
	[SerializeField] BattleManager battleManager;
	[SerializeField] GameObject resetButton;
	[SerializeField] TextMeshProUGUI resetButtonText;
	[SerializeField] GameObject victoryImage;
	[SerializeField] GameObject defeatImage;
    [SerializeField] Image playButtonImage;
	[SerializeField] Button playButton;

    private void Start()
    {
        battleManager.PlayerPartyWon += OnVictory;
        battleManager.EnemyPartyWon += OnDefeat;
    }

    public void StartGame()
    {
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