using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField] Player player;
    bool inIdle = false;

    #region Unity Events
    private void Start()
    {
        player = Player.Get();
    }
    #endregion

    public void LoadMenu()
    {
        if (inIdle)
        {
            inIdle = false;
            player.SaveLogOutDate();
        }
        SceneLoader.LoadMenu();
    }
    public void LoadAutobattle()
    {
        SceneLoader.LoadAutobattle();
    }
    public void LoadIdle()
    {
        inIdle = true;
        player.SaveLogInDate();
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