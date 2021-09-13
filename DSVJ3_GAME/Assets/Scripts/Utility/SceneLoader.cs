using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadAutobattle()
    {
        SceneManager.LoadScene("BattleTest");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}