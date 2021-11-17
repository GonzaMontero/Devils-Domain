using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum Scenes { menu, autobattle, idle, gacha, lineup }
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
            default:
                return Scenes.menu;
        }
    }
    public static void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public static void LoadAutobattle()
    {
        SceneManager.LoadScene("BattleTest");
    }
    public static void LoadIdle()
    {
        SceneManager.LoadScene("Rooms");
    }
    public static void LoadGacha()
    {
        SceneManager.LoadScene("Gacha");
    }
    public static void LoadLineup()
    {
        SceneManager.LoadScene("Lineup");
    }
    //public static void LoadSettings()
    //{
    //    SceneManager.LoadScene("BattleTest");
    //}
}