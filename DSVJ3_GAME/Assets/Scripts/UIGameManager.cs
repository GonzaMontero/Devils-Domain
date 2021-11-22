using UnityEngine;

public class UIGameManager : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Get();
    }

    public void LoadMenu()
    {
        gameManager.LoadMenu();
    }
    public void LoadAutobattle()
    {
        gameManager.LoadAutobattle();
    }
    public void LoadIdle()
    {
        gameManager.LoadIdle();
    }
    public void LoadGacha()
    {
        gameManager.LoadGacha();
    }
    public void LoadLineup()
    {
        gameManager.LoadLineup();
    }
    public void LoadCredits()
    {
        gameManager.LoadCredits();
    }
}