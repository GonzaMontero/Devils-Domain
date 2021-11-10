using UnityEngine;
using TMPro;

public class UIPlayerData : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI gemsText;
	Player player;

    private void Start()
    {
        //Player
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        //Set values
        UpdateData();
    }

    private void OnDisable()
    {
        UpdateData();
    }
    private void OnEnable()
    {
        UpdateData();
    }

    void UpdateData()
    {
        if (player.gold > 0) goldText.text = player.gold.ToString();
        if (player.gems > 0) gemsText.text = player.gems.ToString();
    }
}