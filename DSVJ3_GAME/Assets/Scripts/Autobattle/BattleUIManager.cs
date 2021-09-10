using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIManager : MonoBehaviour
{
	[SerializeField] BattleManager battleManager;
	[SerializeField] Image playButtonImage;
	[SerializeField] Button playButton;

	public void StartGame()
    {
		playButton.enabled = false;
		playButtonImage.color = Color.gray;
		battleManager.readyToStart = true;
    }
}