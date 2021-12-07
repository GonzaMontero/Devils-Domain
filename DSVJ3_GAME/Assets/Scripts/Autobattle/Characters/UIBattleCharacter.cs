using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class UIBattleCharacter : MonoBehaviour
{
    class DamageText
    {
        public Transform transform;
        public TextMeshProUGUI text;
        public int damage;
        public float duration;
    }

    [SerializeField] BattleCharacterController controller;
    [SerializeField] GameObject damageTextPrefab;
    [SerializeField] Transform healthBar;
    [SerializeField] Transform damageTextEmpty;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] int damageTextQuantity;
    [SerializeField] float damageTextSpeed;
    [SerializeField] float damageTextDuration;
    [SerializeField] float damageTextChangeDuration;
    [SerializeField] float damageXRange;
    [SerializeField] List<DamageText> activeDamageTexts;
    List<DamageText> inactiveDamageTexts;

    //Unity Events
    private void Start()
    {
        //Link Actions
        LinkActions();

        //Instantiate all damage text
        activeDamageTexts = new List<DamageText>();
        inactiveDamageTexts = new List<DamageText>();
        InitDamagePopUps();
    }
    private void OnDisable()
    {
        UnlinkActions();
        DeactivateAllDamagePopUps();
    }
    private void OnEnable()
    {
        LinkActions();
    }

    //Methods
    void LinkActions()
    {
        controller.Set += OnLevelUp; //set health and lvl
        controller.DamageReceived += OnRecievedDamage;
        controller.LeveledUp += OnLevelUp;
    }
    void UnlinkActions()
    {
        controller.Set -= OnLevelUp; //set health and lvl
        controller.DamageReceived -= OnRecievedDamage;
        controller.LeveledUp -= OnLevelUp;
    }
    void InitDamagePopUps()
    {
        DamageText newDamageText;
        for (int i = 0; i < damageTextQuantity; i++)
        {
            newDamageText = new DamageText();
            newDamageText.transform = Instantiate(damageTextPrefab, damageTextEmpty).transform;
            newDamageText.transform.gameObject.SetActive(false);
            newDamageText.text = newDamageText.transform.GetComponent<TextMeshProUGUI>();
            newDamageText.duration = 0;
            inactiveDamageTexts.Add(newDamageText);
        }
    }
    void DeactivateDamagePopUp(DamageText damagePopUp)
    {
        if (activeDamageTexts.Count < 1) return;

        //Deactivate
        inactiveDamageTexts.Add(damagePopUp);
        damagePopUp.transform.gameObject.SetActive(false);
        damagePopUp.duration = 0;
        
        //Remove from actives
        activeDamageTexts.Remove(damagePopUp);
    }
    DamageText ActivateDamagePopUp()
    {
        if (inactiveDamageTexts.Count < 1) return null;

        //Activate
        activeDamageTexts.Add(inactiveDamageTexts[0]);
        inactiveDamageTexts[0].transform.gameObject.SetActive(true);

        //Remove from inactives
        inactiveDamageTexts.RemoveAt(0);

        //Return
        return activeDamageTexts[activeDamageTexts.Count - 1];
    }
    void DeactivateAllDamagePopUps()
    {
        if (damageTextEmpty.childCount < 1) return; //return if no damage texts
        StopAllCoroutines();

        //Deactivate all damage texts
        while (activeDamageTexts.Count > 0)
        {
            DeactivateDamagePopUp(activeDamageTexts[0]);
        }
    }

    //Event Recievers
    void OnRecievedDamage(int damage)
    {
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
        if (!controller.publicData.so) return;
        Vector3 newHealthScale = healthBar.localScale;
        newHealthScale.x = controller.GetHealthPercentage();
        healthBar.localScale = newHealthScale;
        healthText.text = controller.publicData.health.ToString();
    }

    IEnumerator ActivateDamageText(int damage)
    {
        DamageText damageText;

        if (activeDamageTexts.Count > 0)
        {
            damageText = activeDamageTexts[activeDamageTexts.Count - 1];
            if (damageText.duration < damageTextChangeDuration)
            {
                damageText.damage += damage;
                damageText.text.text = damageText.damage.ToString();
                yield break;
            }
        }

        if (inactiveDamageTexts.Count < 1) yield break;

        //Activate damage text and get text
        damageText = ActivateDamagePopUp();

        //Set og pos
        damageText.transform.position = levelText.transform.position;
        damageText.transform.Translate(Vector2.up);
        damageText.transform.Translate(Vector2.right * Random.Range(-damageXRange, damageXRange));

        //Set text
        damageText.damage = damage;
        damageText.text.text = damage.ToString();

        //Fade out and move up
        do
        {
            damageText.duration += Time.deltaTime;

            //Make the alpha smaller as the counter goes up (1>0)
            damageText.text.alpha = 1 - damageText.duration / damageTextDuration;

            //Make the text go up slower as the counter goes up (5>0)
            damageText.transform.Translate(Vector2.up * Time.deltaTime * (damageTextSpeed - damageTextSpeed * (damageText.duration / damageTextDuration)));
            yield return null;
        } while (damageText.duration < damageTextDuration);

        //Deactivate text
        DeactivateDamagePopUp(damageText);
        yield break;
    }
}