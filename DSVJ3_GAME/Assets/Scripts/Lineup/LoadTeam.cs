using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadTeam : MonoBehaviour
{
    [SerializeField] GameObject[] lineupFaceImage;
    [SerializeField] TextMeshProUGUI[] lineupDetailsText;
    [SerializeField] Player player;
    [SerializeField] Sprite emptyPlayerSprite;
    int totalHealth;
    int totalDamage;

    private void Start()
    {
        player = Player.Get();

        SetLineup();
    }

    private void OnEnable()
    {
        if (!player) return;
        SetLineup();
    }

    void SetLineup()
    {
        totalHealth = 0;
        totalDamage = 0;

        for (short i = 0; i < player.lineup.Length; i++)
        {
            SetLineupFaceOnIndex(i);
        }

        lineupDetailsText[0].text = "Damage: " + totalDamage;
        lineupDetailsText[1].text = "Health: " + totalHealth;
    }
    void SetLineupFaceOnIndex(int index)
    {
        if (player.lineup[index].so != null)
        {
            lineupFaceImage[index].GetComponent<Image>().sprite = player.lineup[index].so.lineupFaceSprite;
            totalHealth += player.lineup[index].currentStats.maxHealth;
            totalDamage += player.lineup[index].currentStats.damage;
        }
        else
        {
            lineupFaceImage[index].GetComponent<Image>().sprite = emptyPlayerSprite;
        }
        lineupFaceImage[index].GetComponentInParent<StartSwap>().slotOnArray = index;
    }
}
