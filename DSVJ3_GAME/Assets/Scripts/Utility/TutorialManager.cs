using UnityEngine;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] List<GameObject> steps;
    Player player;

    //Unity Events
    private void Start()
    {
        //Get Player and load data
        player = Player.Get();

        //Activate current step
        if (player.tutorialStep >= steps.Count) return; //if final step, return
        steps[player.tutorialStep - 1].SetActive(true);
    }

    //Methods
    void LoadNextStep()
    {
        if (!steps[player.tutorialStep - 1].activeSelf) { return; }

        steps[player.tutorialStep - 1].SetActive(false); //Deactivate current step
        steps[player.tutorialStep]?.SetActive(true); //Activate next step if existant
    }

    //Event Receiver
    public void OnStepUsed(int stepNumber)
    {
        if (stepNumber >= steps.Count) return; //if final step, return
        if (stepNumber != player.tutorialStep) return; //if not current step, return

        player.tutorialStep = stepNumber+1;
        LoadNextStep();
    }
}