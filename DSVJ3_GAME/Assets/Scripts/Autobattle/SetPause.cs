using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPause : MonoBehaviour
{
    private bool pause;
    [SerializeField] GameObject[] buttons;

    public void Pause()
    {
        if (!pause)
        {
            Time.timeScale = 0;
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(true);
            }
            pause = true;
        }
        else
        {
            Time.timeScale = 1;
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
            }
            pause = false;
        }
    }
}
