using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
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