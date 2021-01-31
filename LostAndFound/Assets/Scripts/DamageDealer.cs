using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 5;
    [SerializeField] float delayBetweenAttacks = 1f;
    bool playerWasTouched = false; 

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.GetComponent<Player>())
        {
            GameObject otherGameObject = other.gameObject;
            playerWasTouched = true;
            StartCoroutine(DealDamage(otherGameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.GetComponent<Player>())
        {
            GameObject otherGameObject = other.gameObject;
            playerWasTouched = false;
            StopCoroutine(DealDamage(otherGameObject));
        }
    }

    IEnumerator DealDamage(GameObject otherGameObject)
    {
        while(playerWasTouched)
        {
            Health otherHealth = otherGameObject.GetComponent<Health>();
            otherHealth.TakeDamage(damage);
            if (otherHealth.isDead)
            {
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
            yield return new WaitForSeconds(delayBetweenAttacks);
        }
    }
}
