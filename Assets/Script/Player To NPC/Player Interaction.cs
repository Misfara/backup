using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteraction : MonoBehaviour
{
 NPCInteractable nPCInteractable;

private Vector2 oldMovementInput;
 AgentMover agentMover;
WeaponParent weaponParent;

[SerializeField] private float currentSpeed=0;
Animator animator;

bool canPress;

float delay =0.10f;
public UnityEvent OnDialog,OnDoneDialog;
Rigidbody2D rb;
 public void Awake(){
    nPCInteractable = GameObject.FindGameObjectWithTag("NPC").GetComponent<NPCInteractable>();
    weaponParent = GetComponent<WeaponParent>();
    agentMover = GetComponent<AgentMover>();
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
 }
   
   public void Update (){
    float interactRange = 0.25f;
    Collider2D collider = Physics2D.OverlapCircle(transform.position,interactRange);
    if(collider != null && Input.GetKeyDown(KeyCode.E) ){
        if(collider.TryGetComponent(out NPCInteractable npcInteractable)){
        
            npcInteractable.Update();
            PlayDialog();
            animator.SetFloat("UpDown",0f);
            animator.SetFloat("RightLeft",0f);
            rb.velocity = new Vector2 (0,0).normalized;
            agentMover.dashLocked = true;
          
        } else  if(agentMover.isDashing ==true){
             collider = null;
             interactRange = 0f;
     }
    }
     if(collider != null && Input.GetKeyDown(KeyCode.Q)&& nPCInteractable.isTyping ==  false){
        
  StartCoroutine(JustWait());
        
        agentMover.dashLocked = false;
     }
    

     
   }

    public void PlayDialog(){
        OnDialog?.Invoke ();
      
    }
    public void EndDialog(){
        OnDoneDialog.Invoke();
    }
   
    public IEnumerator JustWait()
    {
           
           yield return new WaitForSeconds(delay);
            EndDialog();
           
      
    }
}
