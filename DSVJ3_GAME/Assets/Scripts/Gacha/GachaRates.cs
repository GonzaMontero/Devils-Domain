using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GachaRates : MonoBehaviour
{
    [SerializeField] GameObject[] gachaBoxesShow;
    [SerializeField] GameObject[] gachaBannerNoShow;
    [SerializeField] GameObject gachaPanel;

    [SerializeField] List<BattleCharacterSO> threeStarCharacters;
    [SerializeField] List<BattleCharacterSO> fourStarCharacters;
    [SerializeField] List<BattleCharacterSO> fiveStarCharacters;

    private void Start()
    {
        BattleCharacterSO[] allCharacters = Resources.LoadAll<BattleCharacterSO>("Scriptable Objects/Characters");

        for (int i = 0; i < allCharacters.Length; i++)
        {
            if(allCharacters[i].numberOfStars == 3)
            {
                threeStarCharacters.Add(allCharacters[i]);
            }
            if (allCharacters[i].numberOfStars == 4)
            {
                fourStarCharacters.Add(allCharacters[i]);
            }
            if (allCharacters[i].numberOfStars == 5)
            {
                fiveStarCharacters.Add(allCharacters[i]);
            }
        }
    }
    private void GachaRoll(int index)
    {
        int randomNumber = UnityEngine.Random.Range(0, 101);

        if(randomNumber>=0 && randomNumber <= 10)
        {
            //ganas 5 estrellas aleatorio
            int r = UnityEngine.Random.Range(0, fiveStarCharacters.Count);
            SendCharacterToBox(index, fiveStarCharacters[r]);
        }
        if (randomNumber >= 11 && randomNumber <= 50)
        {
            //ganas 4 estrellas aleatorio
            int r = UnityEngine.Random.Range(0, fourStarCharacters.Count);
            SendCharacterToBox(index, fourStarCharacters[r]);
        }
        if (randomNumber >= 51 && randomNumber <= 100)
        {
            //ganas 3 estrellas aleatorio
            int r = UnityEngine.Random.Range(0, threeStarCharacters.Count);
            SendCharacterToBox(index, threeStarCharacters[r]);
        }
    }
    public void SendCharacterToBox(int index, BattleCharacterSO character)
    {
        gachaBoxesShow[index].GetComponentInChildren<Image>().sprite = character.sprite;
        gachaBoxesShow[index].GetComponentInChildren<TextMeshProUGUI>().text = character.name;
    }
    private void FindAndLoadBoxes()
    {
        GameObject[] gachaBox = GameObject.FindGameObjectsWithTag("Gacha Boxes");
        gachaBoxesShow = gachaBox;
    }

    #region ButtonPress
    public void PullOnce()
    {
        int index = Mathf.RoundToInt(gachaBoxesShow.Length / 2);
        gachaPanel.SetActive(true);
        FindAndLoadBoxes();
        for (short i = 0; i < gachaBoxesShow.Length; i++)
        {
           gachaBoxesShow[i].SetActive(false);
        }
        for (int i = 0; i < gachaBannerNoShow.Length; i++)
        {
            gachaBannerNoShow[i].SetActive(false);
        }
        gachaBoxesShow[index].SetActive(true);
        GachaRoll(index);
    }
    public void PullEleven()
    {
        int index = 0;
        gachaPanel.SetActive(true);
        FindAndLoadBoxes();
        for (short i = 0; i < gachaBoxesShow.Length; i++)
        {
            gachaBoxesShow[index].SetActive(false);
        }
        for (int i = 0; i < gachaBannerNoShow.Length; i++)
        {
            gachaBannerNoShow[i].SetActive(false);
        }
        for (short i = 0; i < gachaBoxesShow.Length; i++)
        {
            GachaRoll(index);
            gachaBoxesShow[index].SetActive(true);
            index++;
        }
    }
    #endregion
}
