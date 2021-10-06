using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIPlayerData : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI gemsText;
	Player player;
    string sceneName;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //Scene
        sceneName = SceneManager.GetActiveScene().name;
        SceneManager.activeSceneChanged += OnSceneChange;
    }

    
    void OnSceneChange(Scene oldScene, Scene newScene)
    {
        if (newScene.name != sceneName)
        {
            goldText.text = player.gold.ToString();
            gemsText.text = player.gems.ToString();
        }
    }
}