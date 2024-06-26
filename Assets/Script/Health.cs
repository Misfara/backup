using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float delay = 2.3f;
    
    public int damage = 2;

     public int currentHealth=2,maxHealth = 3;

    public UnityEvent<GameObject>OnHitWithReference,OnDeathWithReference;

    [SerializeField] public bool isDead= false;

    Rigidbody2D rb;

    Agent agent;

    Animator animator;

    CapsuleCollider2D capsuleCollider2D;
    BegalHealth begalHealth;

    healthAnimation healthAnimation;

    

public void Awake(){
    animator= GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();
    capsuleCollider2D=GetComponent<CapsuleCollider2D>();
    agent = GetComponent<Agent>();
     begalHealth =GameObject.FindGameObjectWithTag("Enemy").GetComponent<BegalHealth>();
     healthAnimation = GameObject.FindGameObjectWithTag("HealthUI").GetComponent<healthAnimation>();
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

       currentHealth = currentHealth - begalHealth.damage;
    
       

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
