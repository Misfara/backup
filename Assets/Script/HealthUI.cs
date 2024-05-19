using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    Health health;

    healthAnimation healthAnimation;

    public Image []healthUI;
    public Sprite fullheart;
    public Sprite emptyHeart;
    float delay = 0.5f;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        animator = GetComponent<Animator>();
        healthAnimation = GameObject.FindGameObjectWithTag("HealthUI").GetComponent<healthAnimation>();
    }

    public void Update(){

    if(health.currentHealth > health.maxHealth){
        health.currentHealth = health.maxHealth;
    }

    for (int i=0;i< health.maxHealth;i++){
         if( i < health.currentHealth){
            healthUI[i].sprite = fullheart;
         }else{
           StartCoroutine(healthAnimation.Animation());
             healthUI[i].sprite = emptyHeart;
                
            
          
         }
        
    }


}

}
