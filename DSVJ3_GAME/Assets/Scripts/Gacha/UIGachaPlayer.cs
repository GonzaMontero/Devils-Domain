using UnityEngine;
using TMPro;

public class UIGachaPlayer : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI gemsText;
	[SerializeField] GachaPlayer player;


    //Unity Events
    private void Start()
    {
        player.GemsChanged += OnGemsChanged;
    }

    void OnGemsChanged()
    {
		gemsText.text = "Gems: " + player.publicGems;
    }
}