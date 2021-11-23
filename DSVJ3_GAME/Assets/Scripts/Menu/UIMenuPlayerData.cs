using UnityEngine;
using TMPro;

public class UIMenuPlayerData : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI gemsText;
    Player player;

    private void Start()
    {
        //Player 
        player = Player.Get();

        //Set values 
        UpdateData();
    }

    void UpdateData()
    {
        if (player.gold > 0) goldText.text = player.gold.ToString();
        if (player.gems > 0) gemsText.text = player.gems.ToString();
    }
}