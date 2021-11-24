using System;
using UnityEngine;

public class GachaPlayer : MonoBehaviour
{
    public Action GemsChanged;
    public int publicGems { get { return player.gems; } }
	Player player;

    private void Start()
    {
        //Get Player
        player = Player.Get();
        GemsChanged?.Invoke();
    }

    public bool ReduceGems(int price)
    {
        if (player.gems < price) { return false; }
        player.gems -= price;
        GemsChanged?.Invoke();
        return true;
    }
}