using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BegalHealth : MonoBehaviour
{
    public float delay = 1.5f;
    
    public int damage = 1;

    [SerializeField] public int currentHealth,maxHealth;

    public UnityEvent<GameObject>OnHitWithReference,OnDeathWithReference;

    [SerializeField] public bool isDead= false;

    Rigidbody2D rb;

    Agent agent;

    Animator animator;

    CapsuleCollider2D capsuleCollider2D;

    PlayerExperience playerExperience;

    

public void Awake(){
    animator= GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();
    capsuleCollider2D=GetComponent<CapsuleCollider2D>();
    agent = GetComponent<Agent>();

    playerExperience =GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExperience>();
}



    public void InitializeHealth(int healthValue){
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead= false;
    }

    public void GetHit(int amount,GameObject sender)
    {   if(isDead)
        return;
        if(sender.layer == gameObject.layer)
        return;

       currentHealth = currentHealth - playerExperience.Damage();

       

        if(currentHealth >0 ){
            OnHitWithReference?.Invoke(sender);
            
        }else{
            StopAllCoroutines();
            rb.velocity = new UnityEngine.Vector3 (0,0);
            OnDeathWithReference?.Invoke(sender);
            StartCoroutine(Death());
            
        }

    }     

    public IEnumerator Death(){
     
        animator.SetTrigger("Death");
        isDead =true;
        yield return new  WaitForSeconds(delay);
        Destroy(gameObject);
    }

    public void AddHealth(int healthBoost = 2)
    {
        
        int val = currentHealth+ healthBoost;
        currentHealth = val ;
    }

   
}
