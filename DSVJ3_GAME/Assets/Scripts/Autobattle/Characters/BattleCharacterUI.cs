using UnityEngine;
using TMPro;
using System.Collections;

public class BattleCharacterUI : MonoBehaviour
{
    [SerializeField] BattleCharacterController controller;
    [SerializeField] GameObject damageTextPrefab;
    [SerializeField] Transform canvas;
    [SerializeField] Transform healthBar;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI levelText;
    Animator animator;
    bool characterPositioned;
    const float damageTextDuration = 2;
    const float damageXRange = 2;

    //Unity Events
    private void Start()
    {
        //Get Components
        animator = GetComponent<Animator>();

        //Link Actions
        controller.Set += OnSet;
        controller.DamageReceived += OnRecievedDamage;
        controller.LeveledUp += OnLevelUp;
        controller.SearchForTarget += OnSelectTarget;
        controller.Attack += OnAttack;
        controller.Die += OnDeath;

        //Set Defaults
        OnSet();
    }

    //Methods
    void SetSpriteAndAnimations()
    {
        if (controller.publicData.so.sprite)
        {
            GetComponent<SpriteRenderer>().sprite = controller.publicData.so.sprite;
        }
        if (controller.publicData.so.animatorOverride)
        {
            animator.runtimeAnimatorController = controller.publicData.so.animatorOverride;
        }
    }

    //Event Recievers
    void OnSet()
    {
        OnLevelUp();
        SetSpriteAndAnimations();
        animator.SetTrigger("Set");
    }
    void OnRecievedDamage(int damage)
    {
        animator.SetTrigger("Receive Damage");
        OnHealthChanged();
        StartCoroutine(ActivateDamageText(damage));
    }
    void OnLevelUp()
    {
        levelText.text = "LVL " + controller.publicData.level;
        OnHealthChanged();
    }
    void OnHealthChanged()
    {
        Vector3 newHealthScale = healthBar.localScale;
        newHealthScale.x = controller.GetHealthPercentage();
        healthBar.localScale = newHealthScale;
        healthText.text = controller.publicData.health.ToString();
    }
    void OnAttack(int notNeeded)
    {
        animator.SetBool("Attacking", true);

        //Set Sorting layer once battle has started
        if (!characterPositioned)
        {
            transform.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
        characterPositioned = true;
    }
    void OnSelectTarget(BattleCharacterController notNeeded)
    {
        animator.SetBool("Attacking", false);
    }
    void OnDeath(BattleCharacterController notNeeded)
    {
        animator.SetTrigger("Die");
    }

    IEnumerator ActivateDamageText(int damage)
    {
        TextMeshProUGUI damageText = Instantiate(damageTextPrefab, canvas).GetComponent<TextMeshProUGUI>();
        damageText.transform.Translate(Vector2.right * Random.Range(-damageXRange, damageXRange));
        damageText.text = damage.ToString();
        
        float damageCounter = 0;
        do
        {
            damageCounter += Time.deltaTime;
            damageText.alpha = 1 - damageCounter / damageTextDuration;
            damageText.transform.Translate(Vector2.up * Time.deltaTime * (5 - 5 * (damageCounter / damageTextDuration)));
            yield return null;
        } while (damageCounter < damageTextDuration);

        Destroy(damageText.gameObject);
        yield break;
    }
}