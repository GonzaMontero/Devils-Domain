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
        //Player
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        //Scene
        sceneName = SceneManager.GetActiveScene().name;
        SceneManager.activeSceneChanged += OnSceneChange;

        //Set values
        UpdateData();
    }

    
    void OnSceneChange(Scene oldScene, Scene newScene)
    {
        if (newScene.name != sceneName)
        {
            UpdateData();
        }
    }

    void UpdateData()
    {
        if (player.gold > 0) goldText.text = player.gold.ToString();
        if (player.gems > 0) gemsText.text = player.gems.ToString();
    }
}