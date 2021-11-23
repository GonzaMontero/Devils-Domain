using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class UIBattleCharacter : MonoBehaviour
{
    [SerializeField] BattleCharacterController controller;
    [SerializeField] GameObject damageTextPrefab; 
    [SerializeField] Transform healthBar; 
    [SerializeField] Transform damageTextEmpty; 
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] int damageTextQuantity;
    [SerializeField] float damageTextSpeed;
    [SerializeField] float damageTextDuration;
    [SerializeField] float damageXRange;
    List<Transform> activeDamageTexts;
    List<Transform> inactiveDamageTexts;

    //Unity Events
    private void Start()
    {
        //Link Actions
        LinkActions();

        //Instantiate all damage text
        activeDamageTexts = new List<Transform>();
        inactiveDamageTexts = new List<Transform>();
        InitDamagePopUps();

        //Set Defaults
        //OnLevelUp();
        Debug.Log(this);
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
        Transform newDamageText;
        for (int i = 0; i < damageTextQuantity; i++)
        {
            newDamageText = Instantiate(damageTextPrefab, damageTextEmpty).transform;
            newDamageText.gameObject.SetActive(false);
            inactiveDamageTexts.Add(newDamageText);
        }
    }
    void DeactivateDamagePopUp(Transform damagePopUp)
    {
        if (activeDamageTexts.Count < 1) return;

        //Deactivate
        inactiveDamageTexts.Add(damagePopUp);
        damagePopUp.gameObject.SetActive(false);

        //Remove from actives
        activeDamageTexts.Remove(damagePopUp);
    }
    Transform ActivateDamagePopUp()
    {
        if (inactiveDamageTexts.Count < 1) return null;

        //Activate
        activeDamageTexts.Add(inactiveDamageTexts[0]);
        inactiveDamageTexts[0].gameObject.SetActive(true);
        
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
        if (!healthBar) return;
        Vector3 newHealthScale = healthBar.localScale;
        newHealthScale.x = controller.GetHealthPercentage();
        healthBar.localScale = newHealthScale;
        healthText.text = controller.publicData.health.ToString();
    }

    IEnumerator ActivateDamageText(int damage)
    {
        if (inactiveDamageTexts.Count < 1) yield break;
        
        //Activate damage text and get text
        TextMeshProUGUI damageText = ActivateDamagePopUp().GetComponent<TextMeshProUGUI>();

        //Set og pos
        damageText.transform.position = levelText.transform.position;
        damageText.transform.Translate(Vector2.up);
        damageText.transform.Translate(Vector2.right * Random.Range(-damageXRange, damageXRange));
        
        //Set text
        damageText.text = damage.ToString();
        
        //Fade out and move up
        float damageCounter = 0;
        do
        {
            damageCounter += Time.deltaTime;
            
            //Make the alpha smaller as the counter goes up (1>0)
            damageText.alpha = 1 - damageCounter / damageTextDuration; 
            
            //Make the text go up slower as the counter goes up (5>0)
            damageText.transform.Translate(Vector2.up * Time.deltaTime * (damageTextSpeed - damageTextSpeed * (damageCounter / damageTextDuration))); 
            yield return null;
        } while (damageCounter < damageTextDuration);

        //Deactivate text
        DeactivateDamagePopUp(damageText.transform);
        yield break;
    }
}