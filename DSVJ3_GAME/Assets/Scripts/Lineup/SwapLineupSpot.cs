using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapLineupSpot : MonoBehaviour
{
    private int positionOnArray;
    private Player player;

    private void Start()
    {
        player = Player.Get();
    }

    public void swapPositionOnArray(BattleCharacterData characterToSwap)
    {
        player.SwapPositions(positionOnArray, characterToSwap);
    }
}
