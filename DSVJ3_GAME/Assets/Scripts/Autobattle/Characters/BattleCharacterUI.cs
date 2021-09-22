using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class BattleCharacterUI : MonoBehaviour
{
    [SerializeField] BattleCharacterController controller;
    [SerializeField] GameObject damageTextPrefab;
    [SerializeField] Transform canvas;
    [SerializeField] Transform healthBar;
    [SerializeField] TextMeshProUGUI levelText;
    //AnimationClipOverrider animationOverride;
    Animator animator;
    bool characterPositioned;
    const float damageTextDuration = 2;
    const float damageXRange = 2;

    private void Start()
    {
        //Get Components
        animator = GetComponent<Animator>();

        //Set Animator
        //animatorOverride = new AnimatorOverrideController(animator.runtimeAnimatorController);
        //animator.runtimeAnimatorController = animatorOverride; //set runtime animator as animatorOverride
        //animationOverride = new AnimationClipOverrider(animatorOverride.overridesCount);
        //animatorOverride.GetOverrides(animationOverride);

        //Link Actions
        controller.DamageReceived += OnRecievedDamage;
        controller.LeveledUp += OnLevelUp;
        controller.SelectTarget += OnSelectTarget;
        controller.Attack += OnAttack;
        controller.Die += OnDeath;

        //Set Defaults
        levelText.text = "LVL " + controller.publicData.level;
        SetSpriteAndAnimations();
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