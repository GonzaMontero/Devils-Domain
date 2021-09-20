using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviourSingleton<Player>
{
    public List<BattleCharacterController> characters;
    private void Start()
    {
        characters = new List<BattleCharacterController>();
    }

    public void DestroyGameobject()
    {
        Destroy(gameObject);
    }
}
