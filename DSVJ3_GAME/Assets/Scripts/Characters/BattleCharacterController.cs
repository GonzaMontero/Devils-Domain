using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BattleCharacterController : MonoBehaviour
{
    [Serializable] enum States { idle, selectTarget, attacking, dead };

    public Action<BattleCharacterController> Die;
    public Action<BattleCharacterController> SelectTarget;
    public Action<int> Attack;
    public BattleCharacterController target;
    [SerializeField] States current;
    [SerializeField] BattleCharacterData data;
    Animator animator;
    const float despawnTimer = 2;

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

    public void SetData(BattleCharacterSO so)
    {
        data.so = so;
        data.SetLevel0Currents();
        data.SetStartOfBattleCurrents();
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
        animator.SetTrigger("Receive Damage");
        data.health -= damage * ((100 - data.currentStats.armor) / 100); //reduce damage by armor rate
        if (data.health < 0)
        {
            current = States.dead;
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
                animator.SetBool("Attacking", true);
                current++;
                break;
            case States.attacking:
                if (!target || !target.IsAlive())
                {
                    Attack -= target.OnAttack;
                    animator.SetBool("Attacking", false);
                    current = States.idle;
                    return;
                }
                ChargeAttack(data.currentStats.attackSpeed * Time.deltaTime);
                break;
            case States.dead:
                animator.SetTrigger("Die");
                Die.Invoke(this);
                Invoke("DeSpawn", despawnTimer);
                break;
        }
    }
    float attackCooldown = 0;
    void ChargeAttack(float attackCharge)
    {
        attackCooldown += attackCharge;
        if (attackCooldown > 1)
        {
            Attack?.Invoke(data.currentStats.damage);
            attackCooldown = 0;
        }
    }
    void DeSpawn()
    {
        Destroy(gameObject);
    }
}