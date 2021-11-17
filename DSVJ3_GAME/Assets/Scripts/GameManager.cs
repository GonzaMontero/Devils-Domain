using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField] Player player;
    SceneLoader.Scenes currentScene;

    #region Unity Events
    private void Start()
    {
        player = Player.Get();
        currentScene = SceneLoader.GetCurrentScene();
    }
    #endregion

    public void LoadMenu()
    {
        switch (currentScene)
        {
            case SceneLoader.Scenes.idle:
                player.SaveLogOutDate();
                break;
            default:
                break;
        }
        currentScene = SceneLoader.Scenes.menu;
        SceneLoader.LoadMenu();
    }
    public void LoadAutobattle()
    {
        currentScene = SceneLoader.Scenes.autobattle;
        SceneLoader.LoadAutobattle();
    }
    public void LoadIdle()
    {
        currentScene = SceneLoader.Scenes.idle;
        player.SaveLogInDate();
        SceneLoader.LoadIdle();
    }
    public void LoadGacha()
    {
        currentScene = SceneLoader.Scenes.gacha;
        SceneLoader.LoadGacha();
    }
    public void LoadLineup()
    {
        currentScene = SceneLoader.Scenes.lineup;
        SceneLoader.LoadLineup();
    }
}