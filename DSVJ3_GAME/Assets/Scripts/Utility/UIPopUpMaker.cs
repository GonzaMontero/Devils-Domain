using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;

public class UIPopUpMaker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] GameObject popUp;
    TextMeshProUGUI popUpText;
    Image popUpPanel;

    #region Unity Events
    private void Start()
    {
        popUpText = popUp.GetComponentInChildren<TextMeshProUGUI>();
        popUpPanel = popUp.GetComponent<Image>();
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        TurnOnPopUp();
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        TurnOffPopUp(); 
    }
    #endregion

    void TurnOnPopUp()
    {
        StopCoroutine(Fade(false)); //stop fade off before anything else

        StartCoroutine(Fade(true));
    }
    void TurnOffPopUp()
    {
        StopCoroutine(Fade(true)); //stop fade in before anything else

        StartCoroutine(Fade(false));
    }

    IEnumerator Fade(bool fadeIn)
    {
        //Create modifiable colors
        Color panelColor = popUpPanel.color;
        Color textColor = popUpText.color;
        //Get alphas
        float targetAlpha = fadeIn ? 1 : 0;
        float originalPanelAlpha = popUpPanel.color.a;
        float originalTextAlpha = popUpText.color.a;
        //Set timers
        float timer = 0;
        float timerMax = 2;

        if (fadeIn)
        {
            popUp.SetActive(true);
        }

        do
        {
            panelColor.a = Mathf.Lerp(originalPanelAlpha, targetAlpha, timer / timerMax);
            textColor.a = Mathf.Lerp(originalTextAlpha, targetAlpha, timer / timerMax);
            popUpPanel.color = panelColor;
            popUpText.color = textColor;
            timer += Time.deltaTime;
            yield return null;
        } while (timer <= timerMax);

        if (!fadeIn)
        {
            popUp.SetActive(false);
        }
        yield break;
    }
}