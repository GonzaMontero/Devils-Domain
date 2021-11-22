using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] List<GameObject> steps;
    [SerializeField] int currentStep;
    Player player;

    //Unity Events
    private void Start()
    {
        //Get Player and load data
        player = Player.Get();
        currentStep = player.tutorialStep;

        //Activate current step
        LoadNextStep();
    }
    private void OnDestroy()
    {
        player.tutorialStep = currentStep;
    }

    //Methods
    void LoadNextStep()
    {
        if (!steps[currentStep - 1].activeSelf) { return; }

        steps[currentStep - 1].SetActive(false); //Deactivate current step
        steps[currentStep]?.SetActive(true); //Activate next step if existant
    }

    //Event Receiver
    public void OnStepUsed(int stepNumber)
    {
        currentStep++;
        LoadNextStep();
    }
}