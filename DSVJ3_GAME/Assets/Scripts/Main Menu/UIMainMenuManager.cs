using System;
using UnityEngine;

public class UIMainMenuManager : MonoBehaviour
{
    public Action AutoBattleButtonPressed;
    public Action IdleButtonPressed;
    public Action LineupButtonPressed;
    public Action GachaButtonPressed;
    [SerializeField] GameObject underMainteinanceBanner;
    [SerializeField] GameObject mainMenuBanner;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Get();
        AutoBattleButtonPressed += gameManager.LoadAutobattle;
        IdleButtonPressed += gameManager.LoadIdle;
        LineupButtonPressed += gameManager.LoadLineup;
        GachaButtonPressed += gameManager.LoadGacha;
    }
    public void GoToAdventure()
    {
        AutoBattleButtonPressed.Invoke();
    }
    public void GoToIdle()
    {
        IdleButtonPressed.Invoke();
    }
    public void GoToLineup()
    {
        LineupButtonPressed.Invoke();
    }
    public void GoToGacha()
    {
        GachaButtonPressed.Invoke();
    }
}
