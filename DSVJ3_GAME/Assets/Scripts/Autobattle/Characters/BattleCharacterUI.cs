using UnityEngine;
using TMPro;
using System.Collections;

public class BattleCharacterUI : MonoBehaviour
{
    [SerializeField] BattleCharacterController controller;
    [SerializeField] GameObject damageTextPrefab;
    [SerializeField] Transform canvas;
    [SerializeField] Transform healthBar;
    Animator animator;
    bool characterPositioned;
    const float damageTextDuration = 2;
    const float damageXRange = 2;

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller.DamageReceived += OnRecievedDamage;
        controller.SelectTarget += OnSelectTarget;
        controller.Attack += OnAttack;
        controller.Die += OnDeath;
    }

    void OnRecievedDamage(int damage)
    {
        animator.SetTrigger("Receive Damage");
        Vector3 newHealthScale = healthBar.localScale;
        newHealthScale.x = controller.GetHealthPercentage();
        healthBar.localScale = newHealthScale;
        StartCoroutine(ActivateDamageText(damage));
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