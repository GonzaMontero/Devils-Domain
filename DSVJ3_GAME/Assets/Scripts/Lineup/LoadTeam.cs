using UnityEngine;
using UnityEngine.UI;

public class LoadTeam : MonoBehaviour
{
    [SerializeField] GameObject[] lineupFaceImage;
    [SerializeField] Player player;
    [SerializeField] Sprite emptyPlayerSprite;

    private void Awake()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        player = Player.Get();
    }

    private void Start()
    {
        for(short i = 0; i < player.lineup.Length; i++)
        {
            if (player.lineup[i].so != null)
            {
                lineupFaceImage[i].GetComponent<Image>().sprite = player.lineup[i].so.lineupFaceSprite;
            }
            else
            {
                lineupFaceImage[i].GetComponent<Image>().sprite = emptyPlayerSprite;
            }

            lineupFaceImage[i].GetComponentInParent<StartSwap>().slotOnArray = i;
        }
    }
}
