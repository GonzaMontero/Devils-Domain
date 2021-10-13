using System;
using UnityEngine;

public class GachaPlayer : MonoBehaviour
{
    public Action GemsChanged;
    public int publicGems { get { return gems; } }
    [SerializeField] int gems;
	Player player;

    #region Unity Events
    private void Start()
    {
        //Get Player
        player = Player.Get();
        gems = player.gems;
    }
    private void OnDestroy()
    {
        player.gems = gems;
    }
    #endregion

    public bool ReduceGems(int price)
    {
        if (gems < price) { return false; }
        gems -= price;
        GemsChanged.Invoke();
        return true;
    }
}