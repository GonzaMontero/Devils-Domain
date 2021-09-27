using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BattlePlayer : MonoBehaviourSingleton<BattlePlayer>
{
    public List<BattleCharacterController> characters;
    //Player player;
    string sceneName;
    private void Start()
    {
        //Characters
        //player = FindObjectOfType<Player>();
        characters = new List<BattleCharacterController>();

        //Scene
        sceneName = SceneManager.GetActiveScene().name;
        SceneManager.activeSceneChanged += OnSceneChange;
    }
    
    void OnSceneChange(Scene oldScene, Scene newScene)
    {
        if (newScene.name != sceneName)
        {
            SceneManager.activeSceneChanged -= OnSceneChange;
            Destroy(gameObject);
        }
    }
}
