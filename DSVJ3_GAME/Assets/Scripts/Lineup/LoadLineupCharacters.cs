using UnityEngine;
using UnityEngine.UI;

public class LoadLineupCharacters : MonoBehaviour
{
    [SerializeField] GameObject[] lineupCharacters;

    void Start()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        for(short i = 0; i < lineupCharacters.Length; i++)
        {
            if(lineupCharacters[i] != null)
            {
                lineupCharacters[i].GetComponent<Image>().sprite = player.lineup[i].so.sprite;
                lineupCharacters[i].GetComponent<LineupHoverActivate>().LoadData(player.lineup[i].so.sprite, player.lineup[i].so.name);
            }
        }
    }
}
