using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LineupHoverActivate : MonoBehaviour
{
    [SerializeField] GameObject[] lineupHover;
    private Sprite CharacterSprite;
    private string CharacterName;

    public void LoadData(Sprite spr, string str)
    {
        CharacterSprite = spr;
        CharacterName = str;
    }

    private void Awake()
    {
        lineupHover = GameObject.FindGameObjectsWithTag("Lineup Hover Holder");
    }

    public void EnterMouse()
    {
        Debug.Log("Hi!");
        StartCoroutine(OnMouseHover());
    }

    public void ExitMouse()
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
            lineupHover[1].SetActive(true);
            lineupHover[1].GetComponentInChildren<Image>().sprite = CharacterSprite;
            lineupHover[1].GetComponentInChildren<Text>().text = CharacterName;
        }
        else
        {
            lineupHover[0].SetActive(true);
            lineupHover[0].GetComponentInChildren<Image>().sprite = CharacterSprite;
            lineupHover[0].GetComponentInChildren<Text>().text = CharacterName;
        }
    }
}
