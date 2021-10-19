using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BattlePlayer : MonoBehaviourSingleton<BattlePlayer>
{
    public List<BattleCharacterController> characters;
    Player player;
    string sceneName;

    private void Start()
    {
        //Player
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        //Get Characters
        GetCharacters();

        //Scene
        sceneName = SceneManager.GetActiveScene().name;
        SceneManager.activeSceneChanged += OnSceneChange;
    }

    public void AddGold(int gold)
    {
        player.gold += gold;
    }

    void GetCharacters()
    {
        for (int i = 0; i < player.lineup.Length; i++)
        {
            GameObject character = characters[i].gameObject;
            BattleCharacterController characterController = character.GetComponent<BattleCharacterController>();
            characters[i].SetData(player.lineup[i]);
            characterController = characters[i];
            characterController.InitCharacter();
        }
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
