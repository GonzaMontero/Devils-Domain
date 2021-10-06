using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LineupHoverActivate : MonoBehaviour
{
    private GameObject[] lineupHover;
    private Sprite CharacterSprite;
    private string CharacterName;

    public void LoadData(Sprite spr, string str)
    {
        CharacterSprite = spr;
        CharacterName = str;
    }

    private void Start()
    {
        lineupHover = GameObject.FindGameObjectsWithTag("Lineup Hover Holder");
    }

    private void OnMouseEnter()
    {
        Debug.Log("You entered the square!");
        StartCoroutine(OnMouseHover());   
    }

    private void OnMouseExit()
    {
        StopCoroutine(OnMouseHover());
        for(short i = 0; i < lineupHover.Length; i++)
        {
            lineupHover[i].SetActive(false);
        }

    }

    IEnumerator OnMouseHover()
    {
        yield return new WaitForSeconds(5);

        if (transform.position.x < Camera.main.transform.position.x)
        {
            lineupHover[2].SetActive(true);
            lineupHover[2].GetComponentInChildren<Image>().sprite = CharacterSprite;
            lineupHover[2].GetComponentInChildren<Text>().text = CharacterName;
        }
        else
        {
            lineupHover[1].SetActive(true);
            lineupHover[1].GetComponentInChildren<Image>().sprite = CharacterSprite;
            lineupHover[1].GetComponentInChildren<Text>().text = CharacterName;
        }
    }
}
