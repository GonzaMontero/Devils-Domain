using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BattleCharacterController : MonoBehaviour
{
    [Serializable] enum States { idle, attacking, dead };

    public Action<BattleCharacterController> Attack;
    public int slotNumber; //TEMP, THINK OF SOMETHING ELSE FOR BATTLE MANAGER
    [SerializeField] States current;
    [SerializeField] BattleCharacterData data;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        current = States.idle;
    }
    void Update()
    {
        RunStateMachine();
    }

    public AttackType GetAttackType()
    {
        return data.so.attackType;
    }
    public int GetAttackDamage()
    {
        return data.currentStats.damage;
    }
    public void ReceiveDamage(int damage)
    {
        data.health -= damage * ((100 - data.currentStats.armor) / 100); //reduce damage by armor rate
    }
    void RunStateMachine()
    {
        switch (current)
        {
            case States.idle:
                break;
            case States.attacking:
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    current = States.idle;
                    return;
                }
                Attack?.Invoke(this);
                break;
            case States.dead:
                break;
        }
    }
    void SetAnimatorSpeeds()
    {

    }
}