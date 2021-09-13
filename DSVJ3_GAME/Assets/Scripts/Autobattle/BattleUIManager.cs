using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIManager : MonoBehaviour
{
	[SerializeField] BattleManager battleManager;
	[SerializeField] GameObject resetButton;
	[SerializeField] GameObject victoryText;
	[SerializeField] GameObject defeatText;
    [SerializeField] Image playButtonImage;
	[SerializeField] Button playButton;

    private void Start()
    {
        battleManager.RightPartyWon += OnVictory;
        battleManager.LeftPartyWon += OnDefeat;
    }

    public void StartGame()
    {
		playButton.enabled = false;
		playButtonImage.color = Color.gray;
        battleManager.StartGame();
    }

    void OnDefeat()
    {
        resetButton.SetActive(true);
        defeatText.SetActive(true);
    }
    void OnVictory()
    {
        resetButton.SetActive(true);
        victoryText.SetActive(true);
    }
}