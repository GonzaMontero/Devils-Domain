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
        SceneLoader.LoadScene(currentScene);
    }
    public void LoadAutobattle()
    {
        currentScene = SceneLoader.Scenes.autobattle;
        SceneLoader.LoadScene(currentScene);
    }
    public void LoadIdle()
    {
        currentScene = SceneLoader.Scenes.idle;
        player.SaveLogInDate();
        SceneLoader.LoadScene(currentScene);
    }
    public void LoadGacha()
    {
        currentScene = SceneLoader.Scenes.gacha;
        SceneLoader.LoadScene(currentScene);
    }
    public void LoadLineup()
    {
        currentScene = SceneLoader.Scenes.lineup;
        SceneLoader.LoadScene(currentScene);
    }
    public void LoadCredits()
    {
        currentScene = SceneLoader.Scenes.credits;
        SceneLoader.LoadScene(currentScene);
    }
}