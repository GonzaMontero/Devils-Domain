using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToSummon : MonoBehaviour
{
    [SerializeField] GameObject gachaPanel;
    [SerializeField] GameObject[] gachaSummonsScreen;

    private void OnMouseDown()
    {
        gachaPanel.SetActive(false);
        for(short i = 0; i < gachaSummonsScreen.Length; i++)
        {
            gachaSummonsScreen[i].SetActive(true);
        }
    }
}
