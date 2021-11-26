using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadTeam : MonoBehaviour
{
    [SerializeField] GameObject[] lineupFaceImage;
    [SerializeField] TextMeshProUGUI[] lineupDetailsText;
    [SerializeField] Player player;
    [SerializeField] Sprite emptyPlayerSprite;

    private void Awake()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        player = Player.Get();
    }

    private void Start()
    {
        int totalHealth = 0;
        int totalDamage = 0;
        for(short i = 0; i < player.lineup.Length; i++)
        {
            if (player.lineup[i].so != null)
            {
                lineupFaceImage[i].GetComponent<Image>().sprite = player.lineup[i].so.lineupFaceSprite;
                totalHealth += player.lineup[i].currentStats.maxHealth;
                totalDamage += player.lineup[i].currentStats.damage;
            }
            else
            {
                lineupFaceImage[i].GetComponent<Image>().sprite = emptyPlayerSprite;
            }
            lineupFaceImage[i].GetComponentInParent<StartSwap>().slotOnArray = i;
        }
       
        lineupDetailsText[0].text = "Damage: " + totalDamage;
        lineupDetailsText[1].text = "Health: " + totalHealth;
    }

    private void OnEnable()
    {
        int totalHealth = 0;
        int totalDamage = 0;
        for (short i = 0; i < player.lineup.Length; i++)
        {
            if (player.lineup[i].so != null)
            {
                lineupFaceImage[i].GetComponent<Image>().sprite = player.lineup[i].so.lineupFaceSprite;
                totalHealth += player.lineup[i].currentStats.maxHealth;
                totalDamage += player.lineup[i].currentStats.damage;
            }
            else
            {
                lineupFaceImage[i].GetComponent<Image>().sprite = emptyPlayerSprite;
            }
            lineupFaceImage[i].GetComponentInParent<StartSwap>().slotOnArray = i;
        }

        lineupDetailsText[0].text = "Damage: " + totalDamage;
        lineupDetailsText[1].text = "Health: " + totalHealth;
    }
}
