using System;
using UnityEngine;
using TMPro;

public class UICHEATS : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] CHEATS cheatManager;
    [SerializeField] TMP_InputField valueInput;
    [SerializeField] int cheatValue;

    //Unity Events
    private void Start()
    {
        player = Player.Get();
        cheatManager = player.gameObject.GetComponent<CHEATS>();
    }

    //Event Receivers
    public void OnValueInputChanged()
    {
        cheatValue = Convert.ToInt32(valueInput.text);
    }
    public void OnAddGems()
    {
        cheatManager.AddGems(cheatValue);
    }
    public void OnAddGold()
    {
        cheatManager.AddGold(cheatValue);
    }
}