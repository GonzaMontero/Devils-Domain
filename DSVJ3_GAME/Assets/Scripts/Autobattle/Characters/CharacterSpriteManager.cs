using UnityEngine;

public class CharacterSpriteManager : MonoBehaviour
{
	[SerializeField] BattleCharacterController controller;
	SpriteRenderer spriteRenderer;
	Animator animator;

    //Unity Events
    private void Start()
    {
        //Get Components
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        //Link Actions
        controller.Set += OnSet;
        controller.DamageReceived += OnRecievedDamage;
        controller.SearchForTarget += OnSelectTarget;
        controller.Attack += OnAttack;
        controller.Die += OnDeath;

        //Set Defaults
        SetSpriteAndAnimations();
    }

    //Methods
    public void SetSpriteOrder(int order, string layer = null)
    {
        spriteRenderer.sortingOrder = order;

        if (layer == null) return;
        spriteRenderer.sortingLayerName = layer;
    }
    void SetSpriteAndAnimations()
    {
        if (!controller.publicData.so) return; //if character not initialized, return

        if (controller.publicData.so.sprite)
        {
            spriteRenderer.sprite = controller.publicData.so.sprite;
        }
        if (controller.publicData.so.animatorOverride)
        {
            animator.runtimeAnimatorController = controller.publicData.so.animatorOverride;
        }
    }

    //Event Recievers
    void OnSet()
    {
        //Debug.Log(gameObject.name + "Set Animation");
        if (controller.publicData.so.sprite)
        {
            GetComponent<SpriteRenderer>().sprite = controller.publicData.so.sprite;
        }
        animator.SetBool("Dead", false);
    }
    void OnRecievedDamage(int damage)
    {
        //Debug.Log(this);
        animator.SetTrigger("Receive Damage");
    }
    void OnAttack(int notNeeded)
    {
        animator.SetBool("Attacking", true);
        
        ////Set Sorting layer once battle has started CHECK
        //if (!characterPositioned)
        //{
        //    transform.GetComponent<SpriteRenderer>().sortingOrder = 2;
        //}
        //characterPositioned = true;
    }
    void OnSelectTarget(BattleCharacterController notNeeded)
    {
        animator.SetBool("Attacking", false);
    }
    void OnDeath(BattleCharacterController notNeeded)
    {
        animator.SetBool("Dead", true);
    }
}