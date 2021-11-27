using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField] Player player;
    SceneLoader.Scenes currentScene;

    //Unity Events
    private void Start()
    {
        player = Player.Get();
        currentScene = SceneLoader.GetCurrentScene();
        switch (currentScene)
        {
            case SceneLoader.Scenes.menu:
                AkSoundEngine.SetState("Music", "Menu");
                AkSoundEngine.PostEvent("MenuMusic", gameObject);
                break;
            case SceneLoader.Scenes.credits:
                AkSoundEngine.PostEvent("MenuMusic", gameObject);
                break;
            case SceneLoader.Scenes.autobattle:
                AkSoundEngine.SetState("Autobattle", "Combat");
                AkSoundEngine.PostEvent("AutobattleMusic", gameObject);
                break;
            case SceneLoader.Scenes.idle:
                AkSoundEngine.SetState("Music", "Idle");
                AkSoundEngine.PostEvent("MenuMusic", gameObject);
                break;
            case SceneLoader.Scenes.gacha:
                AkSoundEngine.PostEvent("MenuMusic", gameObject);
                break;
            case SceneLoader.Scenes.lineup:
                AkSoundEngine.PostEvent("MenuMusic", gameObject);
                break;
            case SceneLoader.Scenes.settings:
                AkSoundEngine.PostEvent("MenuMusic", gameObject);
                break;
            default:
                break;
        }
    }
    private void OnDestroy()
    {
        if (GameManager.Get() == this)
        {
            QuitGame();
        }
    }

    //Methods
    public void LoadMenu()
    {
        switch (currentScene)
        {
            case SceneLoader.Scenes.idle:
                player.SaveLogOutDate();
                AkSoundEngine.SetState("Music", "Menu");
                break;
            case SceneLoader.Scenes.autobattle:
                AkSoundEngine.SetState("Autobattle", "None");
                break;
            default:
                break;
        }
        currentScene = SceneLoader.Scenes.menu;
        SceneLoader.LoadScene(currentScene);
    }
    public void LoadAutobattle()
    {
        if (currentScene == SceneLoader.Scenes.menu)
        {
            AkSoundEngine.SetState("Music", "None");
        }
        currentScene = SceneLoader.Scenes.autobattle;
        SceneLoader.LoadScene(currentScene);
    }
    public void LoadIdle()
    {
        if (currentScene == SceneLoader.Scenes.menu)
        {
            AkSoundEngine.SetState("Music", "Idle");
        }
        currentScene = SceneLoader.Scenes.idle;
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
    public void LoadSettings()
    {
        currentScene = SceneLoader.Scenes.settings;
        SceneLoader.LoadScene(currentScene);
    }
    public void QuitGame()
    {
        if (currentScene == SceneLoader.Scenes.idle)
        {
            player?.SaveLogOutDate();
        }
        player?.SaveData();
        Application.Quit();
    }
}