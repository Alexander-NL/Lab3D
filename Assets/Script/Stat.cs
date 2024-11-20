using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    Animator animator;
    const string DEATH = "Death";
    
    public float maxHealth;
    public float currentHealth;
    
    void Awake(){
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;         
    }

    public void TakeDamage(int ammount){
        currentHealth -= ammount;
        if(currentHealth <= 0){
            animator.Play(DEATH);
            StartCoroutine(DelayedDeath(5f));
        }
    }

    IEnumerator DelayedDeath(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    void Death(){
        Destroy(gameObject);
    }
}
