using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArea : MonoBehaviour
{
  bool canChase = false;

 TargetDetector targetDetector;
 Agent agent;

 EnemyAI enemyAI;
 
    void Start()
    {
        targetDetector= GameObject.FindGameObjectWithTag("Detector").GetComponent<TargetDetector>();
        agent= GameObject.FindGameObjectWithTag("Player").GetComponent<Agent>();
        enemyAI =GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void OnTriggerEnter2D (Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
                targetDetector.targetDetectionRange =6f;
                 enemyAI.attackDistanceThreshold =1.5f;
               enemyAI.chaseDistanceThreshold =1.5f;
                canChase = true;
        }

       
    }

    public void OnTriggerStay2D (Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
                targetDetector.targetDetectionRange =6f;
                enemyAI.attackDistanceThreshold =1.5f;
               enemyAI.chaseDistanceThreshold =1.5f;
                 canChase = true;
        }
        
    }


      public void OnTriggerExit2D (Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
               targetDetector.targetDetectionRange =0f;
               enemyAI.attackDistanceThreshold =2.5f;
               enemyAI.chaseDistanceThreshold =2.5f;
                 canChase = false;  
        } 
    }
}
