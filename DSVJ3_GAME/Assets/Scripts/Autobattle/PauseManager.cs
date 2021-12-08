using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool pause;
    [SerializeField] GameObject[] menuButtons;
    [SerializeField] GameObject[] pauseButtons;

    private void Start()
    {
        Time.timeScale = 1; //set time scale to 1, in case the game was paused since exit
    }
    private void OnDestroy()
    {
        Time.timeScale = 1; //set time scale to 1, in case the game was paused while quitting
    }

    public void Pause()
    {
        if (!pause)
        {
            Time.timeScale = 0;

            foreach (var button in menuButtons)
            {
                button.SetActive(false);
            }
            for (int i = 0; i < pauseButtons.Length; i++)
            {
                pauseButtons[i].SetActive(true);
            }
            pause = true;
        }
        else
        {
            Time.timeScale = 1;

            foreach (var button in menuButtons)
            {
                button.SetActive(true);
            }
            for (int i = 0; i < pauseButtons.Length; i++)
            {
                pauseButtons[i].SetActive(false);
            }
            pause = false;
        }
    }
}