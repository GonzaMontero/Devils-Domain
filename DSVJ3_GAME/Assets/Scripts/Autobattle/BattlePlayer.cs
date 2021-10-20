using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class BattlePlayer : MonoBehaviourSingleton<BattlePlayer>
{
    public List<BattleCharacterController> characters;
    public Action NewCharactersAdded;
    Player player;
    string sceneName;

    private void Start()
    {
        //Player
        player = Player.Get();

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
            if (characterController.publicData.level >= 1)
            {
                characterController.InitCharacter();
            }
            else
            {
                characterController.InitCharacterFromZero();
            }
        }
        NewCharactersAdded?.Invoke();
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
