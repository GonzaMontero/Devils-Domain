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
    public Action HealthChanged;
    public BattleCharacterController target;
    public BattleCharacterData publicData { get { return data; } }
    [SerializeField] States current;
    [SerializeField] BattleCharacterData data;
    const float despawnTimer = 2;
    const int defaultAttackTime = 1; //attack time in seconds, without attack speed

    private void Awake()
    {
        current = States.idle;
        data.SetLevel1Currents();
        data.SetStartOfBattleCurrents();
    }
    void Update()
    {
        RunStateMachine();
    }

    public void SetData(BattleCharacterSO so)
    {
        data.so = so;
        data.SetLevel1Currents();
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
    public float GetHealthPercentage()
    {
        return (float)data.health / (float)data.currentStats.maxHealth;
    }
    public bool IsAlive()
    {
        return data.health > 0;
    }
    public void ReceiveDamage(int damage)
    {
        int actualDamage = (int)(damage * ((100 - data.currentStats.armor) / 100));
        data.health -= actualDamage; //reduce damage by armor rate
        DamageReceived.Invoke(actualDamage);
        if (data.health <= 0)
        {
            current = States.dead;
        }
    }
    public void ReceiveXP(int xp)
    {
        data.currentXP += xp;
        if (data.currentXP > data.currentXpToLevelUp)
        {
            LevelUp();
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
        if (attackCooldown > defaultAttackTime)
        {
            Attack?.Invoke(data.currentStats.damage);
            attackCooldown = 0;
        }
    }
    void DeSpawn()
    {
        Destroy(gameObject);
    }
    void LevelUp()
    {
        data.currentXP -= data.currentXpToLevelUp;
        data.currentXpToLevelUp += data.so.xpToLevelUpModifier;
        switch (data.so.attackType)
        {
            case AttackType.melee:
                data.currentStats.maxHealth += data.so.baseXpToLevelUp;
                data.currentStats.armor += 0.25f;
                break;
            case AttackType.assasin:
                data.currentStats.attackSpeed += data.so.baseXpToLevelUp / 20;
                data.currentStats.damage += data.so.baseXpToLevelUp / 5;
                break;
            case AttackType.ranged:
                data.currentStats.damage += data.so.baseXpToLevelUp / 2;
                data.currentStats.maxHealth += data.so.baseXpToLevelUp / 5;
                break;
            default:
                break;
        }

        //Keep Leveling Up until character doesn't have any more xp
        if (data.currentXP >= data.currentXpToLevelUp)
        {
            LevelUp();
        }

        data.health = data.currentStats.maxHealth;
        HealthChanged?.Invoke();
    }
}