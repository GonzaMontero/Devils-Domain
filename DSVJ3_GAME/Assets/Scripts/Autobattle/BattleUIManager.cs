using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIManager : MonoBehaviour
{
	[SerializeField] BattleManager battleManager;
	[SerializeField] GameObject resetButton;
	[SerializeField] TextMeshProUGUI resetButtonText;
	[SerializeField] GameObject victoryText;
	[SerializeField] GameObject defeatText;
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

    void OnDefeat()
    {
        resetButtonText.text = "Try Again!";
        resetButton.SetActive(true);
        defeatText.SetActive(true);
    }
    void OnVictory()
    {
        resetButtonText.text = "Next Level";
        resetButton.SetActive(true);
        victoryText.SetActive(true);
    }
}