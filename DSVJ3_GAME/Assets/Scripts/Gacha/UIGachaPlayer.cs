using UnityEngine;
using TMPro;

public class UIGachaPlayer : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI gemsText;
	[SerializeField] GachaPlayer player;


    #region Unity Events
    private void Start()
    {
        player.GemsChanged += OnGemsChanged;
        OnGemsChanged();
    }
    #endregion

    void OnGemsChanged()
    {
		gemsText.text = "Gems: " + player.publicGems;
    }
}