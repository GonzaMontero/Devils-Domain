using System.Collections.Generic;
using UnityEngine;

public class ReturnToSummon : MonoBehaviour
{
    [SerializeField] GameObject gachaPanel;
    [SerializeField] GameObject gachaSummonsScreen;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gachaPanel.SetActive(false);
            gachaSummonsScreen.SetActive(true);
        }
    }
}
