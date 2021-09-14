using UnityEngine;
using System.Collections.Generic;

public class GachaRates : MonoBehaviour
{
    [SerializeField] GameObject[] gachaBoxesShow;

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
            int r = UnityEngine.Random.Range(0, fiveStarCharacters.Count + 1);
            SendCharacterToBox(index, fiveStarCharacters[r]);
        }
        if (randomNumber >= 11 && randomNumber <= 50)
        {
            //ganas 4 estrellas aleatorio
            int r = UnityEngine.Random.Range(0, fourStarCharacters.Count + 1);
            SendCharacterToBox(index, fourStarCharacters[r]);
        }
        if (randomNumber >= 51 && randomNumber <= 100)
        {
            //ganas 3 estrellas aleatorio
            int r = UnityEngine.Random.Range(0, threeStarCharacters.Count + 1);
            SendCharacterToBox(index, threeStarCharacters[r]);
        }
    }
    public void SendCharacterToBox(int index, BattleCharacterSO character)
    {
        //Aca se pasa el pj al indice
    }

    #region ButtonPress
    public void PullOnce()
    {
        int index = 0;

        GachaRoll(index);
    }
    public void PullEleven()
    {
        int index = 0;

        for(short i = 0; i < 11; i++)
        {
            GachaRoll(index);
            index++;
        }
    }
    #endregion
}
