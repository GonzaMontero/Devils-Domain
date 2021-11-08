﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GachaRates : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject[] singleSummonLoad;
    [SerializeField] GameObject[] multiSummonLoad;

    [SerializeField] GameObject gachaPanel;

    [Header("Scripts")]
    [SerializeField] GachaPlayer player;

    [Header("Character SOs")]
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
    private BattleCharacterSO GachaRoll()
    {
        int randomNumber = UnityEngine.Random.Range(0, 101);

        if(randomNumber>=0 && randomNumber <= 10)
        {
            //ganas 5 estrellas aleatorio
            int r = UnityEngine.Random.Range(0, fiveStarCharacters.Count);
            return fiveStarCharacters[r];
        }
        if (randomNumber >= 11 && randomNumber <= 50)
        {
            //ganas 4 estrellas aleatorio
            int r = UnityEngine.Random.Range(0, fourStarCharacters.Count);
            return fourStarCharacters[r];
        }
        if (randomNumber >= 51 && randomNumber <= 100)
        {
            //ganas 3 estrellas aleatorio
            int r = UnityEngine.Random.Range(0, threeStarCharacters.Count);
            return threeStarCharacters[r];
        }

        return null;
    }

    #region ButtonPress
    public void PullOnce(int price)
    {
        if (!player.ReduceGems(price)) { return; }
        for(short i = 0; i < singleSummonLoad.Length; i++)
        {
            singleSummonLoad[i].SetActive(true);
        }
        singleSummonLoad[1].GetComponentInChildren<Image>().sprite=GachaRoll().gachaSprite;
    }
    public void PullEleven(int price)
    {
        if (!player.ReduceGems(price)) { return; }
        for (short i = 0; i < multiSummonLoad.Length; i++)
        {
            multiSummonLoad[i].SetActive(true);
        }
        for(short i = 1; i < multiSummonLoad.Length; i++)
        {
            multiSummonLoad[i].GetComponentInChildren<Image>().sprite = GachaRoll().gachaSprite;
        }
    }
    #endregion
}
