using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BattleCharacterController : MonoBehaviour
{
    [Serializable] enum States { idle, selectTarget, attacking, dead };

    public Action<BattleCharacterController> SelectTarget;
    public Action<int> Attack;
    public BattleCharacterController target;
    [SerializeField] States current;
    [SerializeField] BattleCharacterData data;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        current = States.idle;
        data.SetLevel0Currents();
        data.SetStartOfBattleCurrents();
    }
    void Update()
    {
        RunStateMachine();
    }


    public void OnAttack(int damage)
    {
        ReceiveDamage(damage);
    }
    public AttackType GetAttackType()
    {
        return data.so.attackType;
    }
    public int GetHealth()
    {
        return data.health;
    }
    public int GetAttackDamage()
    {
        return data.currentStats.damage;
    }
    public bool IsAlive()
    {
        return data.health > 0;
    }
    public void ReceiveDamage(int damage)
    {
        data.health -= damage * ((100 - data.currentStats.armor) / 100); //reduce damage by armor rate
        if (data.health < 0)
        {
            Destroy(gameObject);
        }
    }
    void RunStateMachine()
    {
        switch (current)
        {
            case States.idle:
                current++;
                break;
            case States.selectTarget:
                SelectTarget?.Invoke(this);
                if (!target) { return; }
                Attack += target.OnAttack;
                animator.SetTrigger("Attack");
                current++;
                break;
            case States.attacking:
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || !target || !target.IsAlive())
                {
                    Attack -= target.OnAttack;
                    current = States.idle;
                    return;
                }
                Invoke("InvokeAttack", data.currentStats.attackSpeed);
                break;
            case States.dead:
                animator.SetTrigger("Die");
                break;
        }
    }
    void InvokeAttack()
    {
        Attack?.Invoke(data.currentStats.damage);
    }
    void SetAnimatorSpeeds()
    {

    }
}