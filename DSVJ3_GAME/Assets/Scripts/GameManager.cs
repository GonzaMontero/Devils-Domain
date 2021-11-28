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

        //Start both tracks
        AudioManager.StartMenuMusic(gameObject);
        AudioManager.StartAutobattleMusic(gameObject);
        
        //Mute the unneeded track and set the right one
        switch (currentScene)
        {
            case SceneLoader.Scenes.menu:
                AudioManager.DisableAutobattleMusic(gameObject);
                AudioManager.SetMenuMusic();
                break;
            case SceneLoader.Scenes.credits:
                AudioManager.DisableAutobattleMusic(gameObject);
                AudioManager.SetMenuMusic();
                break;
            case SceneLoader.Scenes.autobattle:
                AudioManager.DisableMenuMusic(gameObject);
                AudioManager.SetAutobattleMusic();
                break;
            case SceneLoader.Scenes.idle:
                AudioManager.DisableAutobattleMusic(gameObject);
                AudioManager.SetIdleMusic();
                break;
            case SceneLoader.Scenes.gacha:
                AudioManager.DisableAutobattleMusic(gameObject);
                AudioManager.SetMenuMusic();
                break;
            case SceneLoader.Scenes.lineup:
                AudioManager.DisableAutobattleMusic(gameObject);
                AudioManager.SetMenuMusic();
                break;
            case SceneLoader.Scenes.settings:
                AudioManager.DisableAutobattleMusic(gameObject);
                AudioManager.SetMenuMusic();
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
                AudioManager.SetMenuMusic();
                break;
            case SceneLoader.Scenes.autobattle:
                AudioManager.DisableAutobattleMusic(gameObject);
                AudioManager.StartMenuMusic(gameObject);
                AudioManager.SetMenuMusic();
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
            AudioManager.DisableMenuMusic(gameObject);
            AudioManager.StartAutobattleMusic(gameObject);
            AudioManager.SetAutobattleMusic();
        }
        currentScene = SceneLoader.Scenes.autobattle;
        SceneLoader.LoadScene(currentScene);
    }
    public void LoadIdle()
    {
        if (currentScene == SceneLoader.Scenes.menu)
        {
            AudioManager.SetIdleMusic();
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