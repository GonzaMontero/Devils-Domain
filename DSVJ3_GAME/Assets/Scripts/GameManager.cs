using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{

    #region Unity Events

    #endregion

    public void LoadMenu()
    {
        SceneLoader.LoadMenu();
    }
    public void LoadAutobattle()
    {
        SceneLoader.LoadAutobattle();
    }
    public void LoadIdle()
    {
        SceneLoader.LoadIdle();
    }
    public void LoadGacha()
    {
        SceneLoader.LoadGacha();
    }
    public void LoadLineup()
    {
        SceneLoader.LoadLineup();
    }
}