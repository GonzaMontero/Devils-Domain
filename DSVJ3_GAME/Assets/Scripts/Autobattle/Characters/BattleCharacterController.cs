using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BattleCharacterController : MonoBehaviour
{
    [Serializable] enum States { idle, selectTarget, attacking, dead };

    public Action<BattleCharacterController> Die;
    public Action<BattleCharacterController> SelectTarget;
    public Action<int> Attack;
    public Action<int> DamageReceived;
    public BattleCharacterController target;
    [SerializeField] States current;
    [SerializeField] BattleCharacterData data;
    const float despawnTimer = 2;

    private void Awake()
    {
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
    public float GetHealthPercentage()
    {
        return (float)data.health / (float)data.currentStats.maxHealth;
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
        int actualDamage = damage * ((100 - data.currentStats.armor) / 100);
        data.health -= actualDamage; //reduce damage by armor rate
        DamageReceived.Invoke(actualDamage);
        if (data.health <= 0)
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
                current++;
                break;
            case States.attacking:
                if (!target || !target.IsAlive())
                {
                    Attack -= target.OnAttack;
                    current = States.idle;
                    return;
                }
                ChargeAttack(data.currentStats.attackSpeed * Time.deltaTime);
                break;
            case States.dead:
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