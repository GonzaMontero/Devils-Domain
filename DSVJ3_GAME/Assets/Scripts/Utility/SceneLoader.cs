using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum Scenes { menu, credits, autobattle, idle, gacha, lineup, settings }
    public static Scenes GetCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        switch (currentScene.name)
        {
            case "Main Menu":
                return Scenes.menu;
            case "BattleTest":
                return Scenes.autobattle;
            case "Rooms":
                return Scenes.idle;
            case "Gacha":
                return Scenes.gacha;
            case "Lineup":
                return Scenes.lineup;
            case "Settings":
                return Scenes.settings;
            default:
                return Scenes.menu;
        }
    }
    public static void LoadScene(Scenes sceneToLoad)
    {
        switch (sceneToLoad)
        {
            case Scenes.menu:
        SceneManager.LoadScene("Main Menu");
                break;
            case Scenes.credits:
                SceneManager.LoadScene("Credits");
                break;
            case Scenes.autobattle:
        SceneManager.LoadScene("BattleTest");
                break;
            case Scenes.idle:
        SceneManager.LoadScene("Rooms");
                break;
            case Scenes.gacha:
        SceneManager.LoadScene("Gacha");
                break;
            case Scenes.lineup:
        SceneManager.LoadScene("Lineup");
                break;
            case Scenes.settings:
                SceneManager.LoadScene("Settings");
                break;
            default:
                break;
        }
    }
}