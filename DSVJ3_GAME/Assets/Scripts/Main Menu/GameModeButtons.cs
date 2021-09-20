using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeButtons : MonoBehaviour
{
    [SerializeField] GameObject underMainteinanceBanner;
    [SerializeField] GameObject mainMenuBanner;

    public void GoToAdventure()
    {
        SceneManager.LoadScene("BattleTest");
    }
    public void GoToUnderManteinance()
    {
        mainMenuBanner.SetActive(false);
        underMainteinanceBanner.SetActive(true);
    }
    public void GoToMainMenu()
    {
        underMainteinanceBanner.SetActive(false);
        mainMenuBanner.SetActive(true);
    }
    public void GoToIdle()
    {
        SceneManager.LoadScene("Rooms");
    }
}
