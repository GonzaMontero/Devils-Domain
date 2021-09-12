using UnityEngine;
using UnityEngine.SceneManagement;

public class ExtraButtons : MonoBehaviour
{
    [SerializeField] GameObject underMainteinanceBanner;
    [SerializeField] GameObject mainMenuBanner;

    public void GoToGacha()
    {
        SceneManager.LoadScene("Gacha");
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
}
