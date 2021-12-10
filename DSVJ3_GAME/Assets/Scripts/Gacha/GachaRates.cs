using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GachaRates : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject[] singleSummonLoad;
    [SerializeField] GameObject[] multiSummonLoad;

    [SerializeField] GameObject gachaPanel;
    [SerializeField] GameObject panelToHide;

    [Header("Scripts")]
    [SerializeField] GachaPlayer player;
    [SerializeField] Player p;

    [Header("Character SOs")]
    [SerializeField] List<BattleCharacterSO> threeStarCharacters;
    [SerializeField] List<BattleCharacterSO> fourStarCharacters;
    [SerializeField] List<BattleCharacterSO> fiveStarCharacters;

    private void Start()
    {
        p = Player.Get();
        BattleCharacterSO[] allCharacters = Resources.LoadAll<BattleCharacterSO>("Scriptable Objects/Characters/Allies");

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
    private void HidePanel()
    {
        panelToHide.SetActive(false);
    }
    private void AddToPlayer(BattleCharacterSO roll)
    {
        BattleCharacterData newData = new BattleCharacterData(roll);

        for(short i = 0; i < p.lineup.Length; i++)
        {
            if (p.lineup[i].so == null)
            {
                p.lineup[i] = newData;
                return;
            }
        }

        p.characters.Add(newData);
    }
    private bool SearchForCopy(BattleCharacterSO roll)
    {
        for (int i = 0; i < p.characters.Count; i++)
        {
            if(p.characters[i].so == roll)
            {
                p.characters[i].LevelUp();
                return true;
            }
        }
        for (int i = 0; i < p.lineup.Length; i++)
        {
            if (p.lineup[i].so == roll)
            {
                p.lineup[i].LevelUp();
                return true;
            }
        }
        return false;
    }
    //primero buscar copia y subir un nivel
    #region ButtonPress
    public void PullOnce(int price)
    {
        if (!player.ReduceGems(price)) { return; }
        HidePanel();
        for(short i = 0; i < singleSummonLoad.Length; i++)
        {
            singleSummonLoad[i].SetActive(true);
        }
        BattleCharacterSO roll = GachaRoll();      
        if (!SearchForCopy(roll))
        {
            AddToPlayer(roll);
            singleSummonLoad[1].GetComponentInChildren<Image>().sprite = roll.gachaSprite;
        }
        else
        {
            singleSummonLoad[1].GetComponentInChildren<Image>().sprite = roll.gachaLVLUpSprite;
        }
    }
    public void PullEleven(int price)
    {
        if (!player.ReduceGems(price)) { return; }
        HidePanel();
        for (short i = 0; i < multiSummonLoad.Length; i++)
        {
            multiSummonLoad[i].SetActive(true);
        }
        for(short i = 1; i < multiSummonLoad.Length; i++)
        {
            BattleCharacterSO roll = GachaRoll();            
            if (!SearchForCopy(roll))
            {
                AddToPlayer(roll);
                multiSummonLoad[i].GetComponentInChildren<Image>().sprite = roll.gachaSprite;
            }
            else
            {
                multiSummonLoad[i].GetComponentInChildren<Image>().sprite = roll.gachaLVLUpSprite;
            }
        }
    }
    #endregion
}
