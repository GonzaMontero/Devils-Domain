using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadAutobattle()
    {
        SceneManager.LoadScene("BattleTest");
    }
}