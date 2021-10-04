using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviourSingleton<TutorialManager>
{
    [SerializeField] List<GameObject> steps;
    string sceneName;

    private void Start()
    {
        //Get Scene name and link action
        sceneName = SceneManager.GetActiveScene().name;
        SceneManager.activeSceneChanged += OnSceneChange;

        //Set Step 1
        steps[0].SetActive(true);
    }

    public void OnStepUsed(int stepNumber)
    {
        if (!steps[stepNumber - 1].activeSelf) { return; }
        
        steps[stepNumber - 1].SetActive(false); //Deactivate current step
        steps[stepNumber]?.SetActive(true); //Activate next step if existant
    }
    void OnSceneChange(Scene oldScene, Scene newScene) //old scene not used, required for action compatibility
    {
        if (newScene.name != sceneName)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}