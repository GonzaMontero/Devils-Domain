using UnityEngine;
using TMPro;

public class UIMenuPlayerData : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI gemsText;
    [SerializeField] TextMeshProUGUI levelsText;
    [SerializeField] TMP_InputField nameText;
    Player player;

    private void Start()
    {
        //Player 
        player = Player.Get();
        player.GemsChanged += UpdateGems;
        player.GoldChanged += UpdateGold;
        player.LevelChanged += UpdateLevel;

        //Set values 
        UpdateData();
    }
    private void OnDestroy()
    {
        player.GemsChanged -= UpdateGems;
        player.GoldChanged -= UpdateGold;
    }

    void UpdateData()
    {
        if (player.gold > 0) UpdateGold();
        if (player.gems > 0) UpdateGems();
        if (player.level > 0) UpdateLevel();
        if (player.name != "") nameText.text = player.playerName;
    }    
    void UpdateGems()
    {
        gemsText.text = player.gems.ToString();
    }
    void UpdateGold()
    {
        goldText.text = player.gold.ToString();
    }
    void UpdateLevel()
    {
        levelsText.text = player.level.ToString();
    }
}