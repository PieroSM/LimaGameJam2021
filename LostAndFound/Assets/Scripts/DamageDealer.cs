using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 5;
    [SerializeField] float delayBetweenAttacks = 1f;
    [SerializeField] Player player;
    Animator animator;
    bool playerWasTouched = false; 

    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.GetComponent<Player>())
        {
            GameObject otherGameObject = other.gameObject;
            playerWasTouched = true;
            animator.SetBool("isAttacking", true);

            // StartCoroutine(DealDamage(otherGameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.GetComponent<Player>())
        {
            GameObject otherGameObject = other.gameObject;
            playerWasTouched = false;
            animator.SetBool("isAttacking", false);
            // StopCoroutine(DealDamage(otherGameObject));
        }
    }

    public void Attack()
    {
        player.GetComponent<Health>().TakeDamage(damage);    
    }
}
