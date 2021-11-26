using UnityEngine;

public class BattleCharacterAudioManager : MonoBehaviour
{
	[SerializeField] BattleCharacterController controller;
    [SerializeField] AttackType attackSwitch;
    [SerializeField] bool characterIsMale;

    //Unity Events
    private void Start()
    {
        //Link Actions
        controller.Set += OnSet;
        //controller.DamageReceived += OnRecievedDamage;
        controller.Attack += OnAttack;
        //controller.Die += OnDeath;
    }

    //Event Recievers
    void OnSet()
    {
        //Set Variables
        attackSwitch = controller.publicData.so.attackType;
    }
    void OnAttack(int notNeeded)
    {
        switch (attackSwitch)
        {
            case AttackType.melee:
            AkSoundEngine.SetSwitch("Attack", "Blunt", gameObject); //post sound
                break;
            case AttackType.assasin:
            AkSoundEngine.SetSwitch("Attack", "Sword", gameObject); //post sound
                break;
            case AttackType.ranged:
            AkSoundEngine.SetSwitch("Attack", "Spell", gameObject); //post sound
                break;
            default:
                break;
        }
    }
    void OnDeath(BattleCharacterController notNeeded)
    {
        if (characterIsMale)
        {
            AkSoundEngine.SetSwitch("Die", "M", gameObject);
        }
        else
        {
            AkSoundEngine.SetSwitch("Die", "F", gameObject);
        }
    }
}