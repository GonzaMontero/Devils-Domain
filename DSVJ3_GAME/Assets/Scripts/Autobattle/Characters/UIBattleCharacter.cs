using UnityEngine;
using TMPro;
using System.Collections;

public class UIBattleCharacter : MonoBehaviour
{
    [SerializeField] BattleCharacterController controller;
    [SerializeField] GameObject damageTextPrefab; 
    [SerializeField] Transform healthBar; 
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI levelText; 
    const float damageTextSpeed = 5;
    const float damageTextDuration = 2;
    const float damageXRange = 2; 

    //Unity Events
    private void Start()
    {
        //Link Actions
        LinkActions();

        //Set Defaults
        OnLevelUp();
        Debug.Log(this);
    }
    private void OnDisable()
    {
        UnlinkActions();
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
        TextMeshProUGUI damageText = Instantiate(damageTextPrefab, transform).GetComponent<TextMeshProUGUI>();
        damageText.transform.Translate(Vector2.right * Random.Range(-damageXRange, damageXRange));
        damageText.text = damage.ToString();
        
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

        Destroy(damageText.gameObject);
        yield break;
    }
}