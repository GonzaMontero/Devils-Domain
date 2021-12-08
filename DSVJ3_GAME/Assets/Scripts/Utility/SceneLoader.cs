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
        string sceneName = "Menu";

        switch (sceneToLoad)
        {
            case Scenes.menu:
                sceneName = "Main Menu";
                break;
            case Scenes.credits:
                sceneName = "Credits";
                break;
            case Scenes.autobattle:
                sceneName = "Autobattle";
                break;
            case Scenes.idle:
                sceneName = "Rooms";
                break;
            case Scenes.gacha:
                sceneName = "Gacha";
                break;
            case Scenes.lineup:
                sceneName = "Lineup";
                break;
            case Scenes.settings:
                sceneName = "Settings";
                break;
            default:
                break;
        }

        ASyncSceneLoader.Get().StartLoad(sceneName);
    }
}