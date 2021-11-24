using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleCharacterController : MonoBehaviour
{
    [Serializable] enum States { idle, selectTarget, attacking, dead };

    public Action<BattleCharacterController> Die;
    public Action<BattleCharacterController> SearchForTarget;
    public Action<int> Attack;
    public Action<int> DamageReceived;
    public Action LeveledUp;
    public Action Set;
    public BattleCharacterController target;
    public BattleCharacterData publicData { get { return data; } }
    [SerializeField] States current;
    [SerializeField] BattleCharacterData data;
    [SerializeField] float despawnTimer;
    [SerializeField] float minAttackChargeMod;
    [SerializeField] float maxAttackChargeMod;
    [SerializeField] int defaultAttackTime; //attack time in seconds, without attack speed
    float attackCooldown = 0;

    //Unity Events
    private void Start()
    {
        current = States.idle;
    }
    void Update()
    {
        if (publicData.so == null)
        {
            Destroy(gameObject);
        }
        RunStateMachine();
    }

    //Methods
    public void InitCharacter()
    {
        data.SetStartOfBattleCurrents();
        current = States.idle;
        Set?.Invoke();
    }
    public void SetData(BattleCharacterData data)
    {
        this.data = data;
        InitCharacter();
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
                SearchForTarget?.Invoke(this);
                if (!target) { return; }
                current++;
                break;
            case States.attacking:
                if (!target || !target.IsAlive())
                {
                    current = States.idle;
                    return;
                }
                float attackCharge = (data.currentStats.attackSpeed * Time.deltaTime);
                ChargeAttack(Random.Range(minAttackChargeMod, maxAttackChargeMod) * attackCharge);
                break;
            case States.dead:
                Die.Invoke(this);
                AkSoundEngine.PostEvent("Die", gameObject);
                Invoke("DeSpawn", despawnTimer);
                break;
        }
    }
    void ChargeAttack(float attackCharge)
    {
        attackCooldown += attackCharge;
        if (attackCooldown > defaultAttackTime)
        {
            AkSoundEngine.PostEvent("Attack", gameObject);
            target.ReceiveDamage(data.currentStats.damage);
            Attack?.Invoke(data.currentStats.damage);
            attackCooldown = 0;
        }
    }
    void DeSpawn()
    {
        if (IsAlive()) return;
        gameObject.SetActive(false);
    }
    void LevelUp()
    {
        data.currentXP -= data.currentXpToLevelUp;
        data.currentXpToLevelUp += data.so.xpToLevelUpModifier;
        data.level++;
        data.LevelUp();

        //Keep Leveling Up until character doesn't have any more xp
        if (data.currentXP >= data.currentXpToLevelUp)
        {
            LevelUp();
        }

        data.health = data.currentStats.maxHealth;
        LeveledUp?.Invoke();
    }
}